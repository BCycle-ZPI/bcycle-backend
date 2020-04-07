using bcycle_backend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace bcycle_backend.Data
{
    public sealed class BCycleContext : DbContext
    {
        public BCycleContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<GroupTrip> GroupTrips { get; set; }
        public DbSet<GroupTripParticipant> GroupTripParticipants { get; set; }
        public DbSet<GroupTripPoint> GroupTripPoints { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<TripPhoto> TripPhotos { get; set; }
        public DbSet<TripPoint> TripPoints { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trip>()
                .HasMany(e => e.Route)
                .WithOne(tp => tp.Trip)
                .HasForeignKey(tp => tp.TripId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Trip>()
                .HasMany(e => e.Photos)
                .WithOne(tp => tp.Trip)
                .HasForeignKey(tp => tp.TripId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GroupTripParticipant>()
                .HasKey(p => new {p.UserId, p.GroupTripId});

            modelBuilder.Entity<GroupTrip>()
                .HasMany(e => e.Route)
                .WithOne(p => p.GroupTrip)
                .HasForeignKey(p => p.GroupTripId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GroupTrip>()
                .HasMany(e => e.Participants)
                .WithOne(p => p.GroupTrip)
                .HasForeignKey(p => p.GroupTripId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
