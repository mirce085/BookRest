using System.Text;
using BookRest.Data;
using BookRest.Endpoints;
using BookRest.Hubs;
using BookRest.Models;
using BookRest.Models.Enums;
using BookRest.Other;
using BookRest.Services;
using BookRest.Services.Interfaces;
using BookRest.Validators;
using BookRest.Validators.MenuItem;
using BookRest.Validators.Notification;
using BookRest.Validators.OperatingHour;
using BookRest.Validators.Reservation;
using BookRest.Validators.Restaurant;
using BookRest.Validators.RestaurantSection;
using BookRest.Validators.RestaurantTable;
using BookRest.Validators.User;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<AppDbContext>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMenuItemService, MenuItemService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, JwtService>();
builder.Services.AddScoped<IRestaurantTableService, RestaurantTableService>();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddValidatorsFromAssemblyContaining<RestaurantCreateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RestaurantUpdateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UserCreateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UserUpdateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<LoginValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RefreshRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<OperatingHourCreateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<OperatingHourUpdateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ReservationCreateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ReservationUpdateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RestaurantSectionCreateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RestaurantSectionUpdateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RestaurantTableCreateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RestaurantTableUpdateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<MenuItemCreateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<MenuItemUpdateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<NotificationCreateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<NotificationUpdateValidator>();

builder.Services.AddSignalR();
builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey =
            new SymmetricSecurityKey(Convert.FromBase64String(builder.Configuration["JwtSettings:SecretKey"]!))
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", p => p.RequireRole(UserRole.Admin.ToString()));
    options.AddPolicy("OwnerOrAdmin", p => p.RequireRole(
        UserRole.Owner.ToString(),
        UserRole.Admin.ToString()));

    options.AddPolicy("IsRestaurantOwner",
        p => p.AddRequirements(new RestaurantOwnerRequirement()));
});
builder.Services.AddSingleton<IAuthorizationHandler, RestaurantOwnerHandler>();

var app = builder.Build();

using var dbContext = app.Services.GetRequiredService<IDbContextFactory<AppDbContext>>().CreateDbContext();

dbContext.Database.EnsureCreated();
//dbContext.Database.EnsureDeleted();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<ReservationHub>("/hub/reservations");
app.MapUserEndpoints();
app.MapAuthEndpoints();
app.MapRestaurantEndpoints();

app.Run();