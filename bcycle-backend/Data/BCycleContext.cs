using bcycle_backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                .HasMany(e => e.TripPoints)
                .WithOne(tp => tp.Trip).HasForeignKey(tp => tp.TripId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Trip>()
                .HasMany(e => e.TripPhotos)
                .WithOne(tp => tp.Trip).HasForeignKey(tp => tp.TripId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GroupTrip>()
                .HasMany(e => e.Participants)
                .WithOne(p => p.GroupTrip).HasForeignKey(p => p.GroupTripId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<GroupTrip>().ToTable("GroupTrip");
            modelBuilder.Entity<GroupTripParticipant>().ToTable("GroupTripParticipant");
            modelBuilder.Entity<GroupTripPoint>().ToTable("GroupTripPoint");
            modelBuilder.Entity<Trip>().ToTable("Trip");
            modelBuilder.Entity<TripPhoto>().ToTable("TripPhoto");
            modelBuilder.Entity<TripPoint>().ToTable("TripPoint");
        }
    }
}
