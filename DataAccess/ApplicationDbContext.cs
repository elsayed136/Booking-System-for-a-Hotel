using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RoomType>()
                .Property(x => x.MaxClientsAllowed)
                .HasDefaultValue(1);

            modelBuilder.Entity<Room>()
                .Property(x => x.PriceFactor)
                .HasDefaultValue(1D);
            modelBuilder.Entity<Reservation>()
                .Property(x => x.NumberOfPeopel)
                .HasDefaultValue(1);
            modelBuilder.Entity<Reservation>()
                .Property(x => x.Discount)
                .HasDefaultValue(0.0);
            modelBuilder.Entity<Reservation>()
                .Property(x => x.TotalPrice)
                .HasDefaultValue(1.0);
            modelBuilder.Entity<Reservation>()
                .Property(x => x.Status)
                .HasDefaultValue(ReservationStatus.Booked);
        }
    }
}
