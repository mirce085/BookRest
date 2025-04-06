using BookRest.Dtos.Reservation;
using BookRest.Dtos.RestaurantTable;
using BookRest.Other;

namespace BookRest.Services.Interfaces;

public interface IReservationService
{
    Task<OperationResult<ReservationDisplayDto>> CreateReservationAsync(ReservationCreateDto dto);
    Task<OperationResult<ReservationDisplayDto>> GetReservationAsync(int reservationId);
    Task<OperationResult<IEnumerable<ReservationDisplayDto>>> GetAllReservationsAsync();
    Task<OperationResult<ReservationDisplayDto>> UpdateReservationAsync(int reservationId, ReservationUpdateDto dto);
    Task<OperationResult<bool>> CancelReservationAsync(int reservationId);
    Task<OperationResult<IEnumerable<ReservationDisplayDto>>> GetReservationsByRestaurantAsync(int restaurantId, DateTime date);
}
