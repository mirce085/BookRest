using AutoMapper;
using BookRest.Data;
using BookRest.Dtos.Reservation;
using BookRest.Dtos.RestaurantTable;
using BookRest.Hubs;
using BookRest.Models;
using BookRest.Other;
using BookRest.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace BookRest.Services;

public class ReservationService(
    IDbContextFactory<AppDbContext> dbContextFactory,
    IMapper mapper,
    IValidator<ReservationCreateDto> createValidator,
    IValidator<ReservationUpdateDto> updateValidator,
    IHubContext<ReservationHub> hubContext) : IReservationService
{
    public async Task<OperationResult<ReservationDisplayDto>> CreateReservationAsync(ReservationCreateDto dto)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var validationResult = await createValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return OperationResult<ReservationDisplayDto>
                .Fail(string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));
        }

        var user = await dbContext.Users.FindAsync(dto.UserId);
        if (user == null)
            return OperationResult<ReservationDisplayDto>.Fail("User does not exist.");

        var table = await dbContext.RestaurantTables.FindAsync(dto.TableId);
        if (table == null || table.RestaurantId != dto.RestaurantId)
            return OperationResult<ReservationDisplayDto>.Fail("Invalid table or doesn't match restaurant.");

        var endTime = dto.ReservationDateTime.AddMinutes(dto.Duration);
        bool conflict = await dbContext.Reservations.AnyAsync(r =>
            r.TableId == dto.TableId &&
            r.Status != "Cancelled" &&
            r.ReservationDateTime < endTime &&
            r.ReservationDateTime.AddMinutes(r.Duration) > dto.ReservationDateTime
        );
        if (conflict)
            return OperationResult<ReservationDisplayDto>.Fail("Selected table is already reserved during that time.");


        var reservation = mapper.Map<Reservation>(dto);
        reservation.Status = "Pending";

        dbContext.Reservations.Add(reservation);
        await dbContext.SaveChangesAsync();

        var resDto = mapper.Map<ReservationDisplayDto>(reservation);

        await hubContext.Clients.All.SendAsync("ReservationCreated", resDto);

        return OperationResult<ReservationDisplayDto>.Ok(resDto);
    }

    public async Task<OperationResult<ReservationDisplayDto>> GetReservationAsync(int reservationId)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var reservation = await dbContext.Reservations.FindAsync(reservationId);
        if (reservation == null)
            return OperationResult<ReservationDisplayDto>.Fail("Reservation not found.");

        var resDto = mapper.Map<ReservationDisplayDto>(reservation);

        return OperationResult<ReservationDisplayDto>.Ok(resDto);
    }

    public async Task<OperationResult<IEnumerable<ReservationDisplayDto>>> GetAllReservationsAsync()
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var list = await dbContext.Reservations
            .Select(r => mapper.Map<ReservationDisplayDto>(r))
            .ToListAsync();

        return OperationResult<IEnumerable<ReservationDisplayDto>>.Ok(list);
    }

    public async Task<OperationResult<ReservationDisplayDto>> UpdateReservationAsync(int reservationId,
        ReservationUpdateDto dto)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var validationResult = await updateValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return OperationResult<ReservationDisplayDto>.Fail(
                string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage))
            );
        }

        var reservation = await dbContext.Reservations.FindAsync(reservationId);
        if (reservation == null)
            return OperationResult<ReservationDisplayDto>.Fail("Reservation not found.");

        if (dto.ReservationDateTime.HasValue || dto.Duration.HasValue)
        {
            var newStart = dto.ReservationDateTime ?? reservation.ReservationDateTime;
            var newDur = dto.Duration ?? reservation.Duration;
            var newEnd = newStart.AddMinutes(newDur);

            bool conflict = await dbContext.Reservations.AnyAsync(r =>
                r.ReservationId != reservationId &&
                r.TableId == reservation.TableId &&
                r.Status != "Cancelled" &&
                r.ReservationDateTime < newEnd &&
                r.ReservationDateTime.AddMinutes(r.Duration) > newStart
            );
            if (conflict)
                return OperationResult<ReservationDisplayDto>.Fail("Updated reservation time conflicts with another.");
        }

        if (dto.ReservationDateTime.HasValue)
            reservation.ReservationDateTime = dto.ReservationDateTime.Value;
        if (dto.Duration.HasValue)
            reservation.Duration = dto.Duration.Value;
        if (dto.NumberOfGuests.HasValue)
            reservation.NumberOfGuests = dto.NumberOfGuests.Value;
        if (!string.IsNullOrEmpty(dto.Status))
            reservation.Status = dto.Status;
        if (!string.IsNullOrEmpty(dto.SpecialRequests))
            reservation.SpecialRequests = dto.SpecialRequests;

        reservation.UpdatedAt = DateTime.UtcNow;

        await dbContext.SaveChangesAsync();

        var updatedDto = mapper.Map<ReservationDisplayDto>(reservation);

        await hubContext.Clients.All.SendAsync("ReservationUpdated", updatedDto);

        return OperationResult<ReservationDisplayDto>.Ok(updatedDto);
    }

    public async Task<OperationResult<bool>> CancelReservationAsync(int reservationId)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var reservation = await dbContext.Reservations.FindAsync(reservationId);
        if (reservation == null)
            return OperationResult<bool>.Fail("Reservation not found.");

        reservation.Status = "Cancelled";
        reservation.UpdatedAt = DateTime.UtcNow;
        await dbContext.SaveChangesAsync();

        await hubContext.Clients.All.SendAsync("ReservationCancelled", reservationId);

        return OperationResult<bool>.Ok(true);
    }

    public async Task<OperationResult<IEnumerable<ReservationDisplayDto>>> GetReservationsByRestaurantAsync(
        int restaurantId, DateTime date)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var start = date.Date;
        var end = start.AddDays(1);

        var reservations = await dbContext.Reservations
            .Where(r => r.RestaurantId == restaurantId &&
                        r.ReservationDateTime >= start &&
                        r.ReservationDateTime < end)
            .Select(r => mapper.Map<ReservationDisplayDto>(r))
            .ToListAsync();

        return OperationResult<IEnumerable<ReservationDisplayDto>>.Ok(reservations);
    }
}