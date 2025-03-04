using BookRest.Models;
using Microsoft.EntityFrameworkCore;

namespace BookRest.Data;

public class AppDbContext(IConfiguration configuration): DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<RestaurantSection> RestaurantSections { get; set; }
    public DbSet<RestaurantTable> RestaurantTables { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<OperatingHour> OperatingHours { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Tag> Tags { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(configuration.GetConnectionString("Sqlite"));
        
        base.OnConfiguring(optionsBuilder);
    }
}