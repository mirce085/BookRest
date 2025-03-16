using BookRest.Data;
using BookRest.Endpoints;
using BookRest.Validators.Restaurant;
using BookRest.Validators.User;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<AppDbContext>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddValidatorsFromAssemblyContaining<RestaurantCreateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RestaurantUpdateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UserCreateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UserUpdateValidator>();



var app = builder.Build();

using var dbContext = app.Services.GetRequiredService<IDbContextFactory<AppDbContext>>().CreateDbContext();

dbContext.Database.EnsureCreated();
//dbContext.Database.EnsureDeleted();

app.MapUserEndpoints();

app.Run();