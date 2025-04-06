using BookRest.Models;
using Microsoft.EntityFrameworkCore;

namespace BookRest.Data;

public class AppDbContext(IConfiguration configuration) : DbContext
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
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(configuration.GetConnectionString("Sqlite"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(u => u.UserId);
            entity.Property(u => u.UserId).ValueGeneratedOnAdd();
            entity.Property(u => u.Username).IsRequired().HasMaxLength(100);
            entity.Property(u => u.Email).IsRequired().HasMaxLength(255);
            entity.HasIndex(u => u.Email).IsUnique();
            entity.Property(u => u.PhoneNumber).HasMaxLength(20);
            entity.Property(u => u.Password).IsRequired().HasMaxLength(255);
            entity.Property(u => u.Role).IsRequired().HasMaxLength(50);
            entity.Property(u => u.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });
        
        modelBuilder.Entity<RefreshToken>()
            .HasIndex(r => r.Token)
            .IsUnique();

        modelBuilder.Entity<Restaurant>(entity =>
        {
            entity.ToTable("Restaurants");
            entity.HasKey(r => r.RestaurantId);
            entity.Property(r => r.RestaurantId).ValueGeneratedOnAdd();
            entity.Property(r => r.Name).IsRequired().HasMaxLength(255);
            entity.Property(r => r.Description);
            entity.Property(r => r.Address).HasMaxLength(255);
            entity.Property(r => r.City).HasMaxLength(100);
            entity.Property(r => r.State).HasMaxLength(100);
            entity.Property(r => r.Country).HasMaxLength(100);
            entity.Property(r => r.Zip).HasMaxLength(20);
            entity.Property(r => r.Phone).HasMaxLength(20);
            entity.Property(r => r.Email).HasMaxLength(255);
            entity.Property(r => r.Website).HasMaxLength(255);
            entity.Property(r => r.AveragePrice).HasColumnType("decimal(10,2)");
            entity.Property(r => r.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(r => r.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(r => r.Owner)
                .WithMany(u => u.OwnedRestaurants)
                .HasForeignKey(r => r.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<RestaurantSection>(entity =>
        {
            entity.ToTable("RestaurantSections");
            entity.HasKey(rs => rs.SectionId);
            entity.Property(rs => rs.SectionId).ValueGeneratedOnAdd();
            entity.Property(rs => rs.Name).IsRequired().HasMaxLength(100);
            entity.Property(rs => rs.Description);

            entity.HasOne(rs => rs.Restaurant)
                .WithMany(r => r.Sections)
                .HasForeignKey(rs => rs.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<RestaurantTable>(entity =>
        {
            entity.ToTable("RestaurantTables");
            entity.HasKey(rt => rt.TableId);
            entity.Property(rt => rt.TableId).ValueGeneratedOnAdd();
            entity.Property(rt => rt.TableNumber).IsRequired().HasMaxLength(50);
            entity.Property(rt => rt.LocationDescription).HasMaxLength(255);

            entity.HasOne(rt => rt.Restaurant)
                .WithMany(r => r.Tables)
                .HasForeignKey(rt => rt.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(rt => rt.Section)
                .WithMany(rs => rs.Tables)
                .HasForeignKey(rt => rt.SectionId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.ToTable("Reservations");
            entity.HasKey(r => r.ReservationId);
            entity.Property(r => r.ReservationId).ValueGeneratedOnAdd();
            entity.Property(r => r.ReservationDateTime).IsRequired();
            entity.Property(r => r.Duration).IsRequired();
            entity.Property(r => r.NumberOfGuests).IsRequired();
            entity.Property(r => r.Status).IsRequired().HasMaxLength(50);
            entity.Property(r => r.SpecialRequests);
            entity.Property(r => r.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(r => r.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(r => r.Restaurant)
                .WithMany(rst => rst.Reservations)
                .HasForeignKey(r => r.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(r => r.Table)
                .WithMany(rt => rt.Reservations)
                .HasForeignKey(r => r.TableId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<OperatingHour>(entity =>
        {
            entity.ToTable("OperatingHours");
            entity.HasKey(o => o.OperatingHourId);
            entity.Property(o => o.OperatingHourId).ValueGeneratedOnAdd();
            entity.Property(o => o.DayOfWeek).IsRequired();
            entity.Property(o => o.OpenTime).IsRequired();
            entity.Property(o => o.CloseTime).IsRequired();

            entity.HasOne(o => o.Restaurant)
                .WithMany(r => r.OperatingHours)
                .HasForeignKey(o => o.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.ToTable("Notifications");
            entity.HasKey(n => n.NotificationId);
            entity.Property(n => n.NotificationId).ValueGeneratedOnAdd();
            entity.Property(n => n.NotificationType).IsRequired().HasMaxLength(50);
            entity.Property(n => n.ScheduledTime).IsRequired();
            entity.Property(n => n.SentStatus).IsRequired().HasMaxLength(50);
            entity.Property(n => n.SentTime);

            entity.HasOne(n => n.Reservation)
                .WithMany(r => r.Notifications)
                .HasForeignKey(n => n.ReservationId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<MenuItem>(entity =>
        {
            entity.ToTable("MenuItems");
            entity.HasKey(m => m.MenuItemId);
            entity.Property(m => m.MenuItemId).ValueGeneratedOnAdd();
            entity.Property(m => m.Name).IsRequired().HasMaxLength(255);
            entity.Property(m => m.Description);
            entity.Property(m => m.Price).IsRequired().HasColumnType("decimal(10,2)");
            entity.Property(m => m.Category).HasMaxLength(100);

            entity.HasOne(m => m.Restaurant)
                .WithMany(r => r.MenuItems)
                .HasForeignKey(m => m.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.ToTable("Reviews");
            entity.HasKey(r => r.ReviewId);
            entity.Property(r => r.ReviewId).ValueGeneratedOnAdd();
            entity.Property(r => r.Rating).IsRequired();
            entity.Property(r => r.Comment);
            entity.Property(r => r.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(r => r.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(r => r.Restaurant)
                .WithMany(rst => rst.Reviews)
                .HasForeignKey(r => r.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.ToTable("Tags");
            entity.HasKey(t => t.TagId);
            entity.Property(t => t.TagId).ValueGeneratedOnAdd();
            entity.Property(t => t.TagName).IsRequired().HasMaxLength(100);
            entity.HasIndex(t => t.TagName).IsUnique();
            entity.Property(t => t.Description).HasMaxLength(255);
        });

        modelBuilder.Entity<Restaurant>()
            .HasMany(r => r.Tags)
            .WithMany(t => t.Restaurants)
            .UsingEntity<Dictionary<string, object>>(
                "RestaurantTags",
                r => r.HasOne<Tag>()
                    .WithMany()
                    .HasForeignKey("TagId")
                    .OnDelete(DeleteBehavior.Cascade),
                t => t.HasOne<Restaurant>()
                    .WithMany()
                    .HasForeignKey("RestaurantId")
                    .OnDelete(DeleteBehavior.Cascade),
                joinEntity =>
                {
                    joinEntity.HasKey("RestaurantId", "TagId");
                    joinEntity.ToTable("RestaurantTags");
                }
            );
    }
}