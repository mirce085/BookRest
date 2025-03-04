using BookRest.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<AppDbContext>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

using var dbContext = app.Services.GetRequiredService<IDbContextFactory<AppDbContext>>().CreateDbContext();

//dbContext.Database.EnsureCreated();
//dbContext.Database.EnsureDeleted();
app.UseHttpsRedirection();

app.Run();