using System.Text;
using BookRest.Data;
using BookRest.Endpoints;
using BookRest.Hubs;
using BookRest.Models;
using BookRest.Services;
using BookRest.Services.Interfaces;
using BookRest.Validators;
using BookRest.Validators.Restaurant;
using BookRest.Validators.User;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<AppDbContext>();   
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddValidatorsFromAssemblyContaining<RestaurantCreateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RestaurantUpdateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UserCreateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UserUpdateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<LoginValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RefreshRequestValidator>();
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
        IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(builder.Configuration["JwtSettings:SecretKey"]!))
    };
});
builder.Services.AddAuthorization();


var app = builder.Build();

app.MapHub<ReservationHub>("/hub/reservations");

using var dbContext = app.Services.GetRequiredService<IDbContextFactory<AppDbContext>>().CreateDbContext();

//dbContext.Database.EnsureCreated();
dbContext.Database.EnsureDeleted();

app.UseAuthentication();
app.UseAuthorization();

app.MapUserEndpoints();
app.MapAuthEndpoints();

app.Run();