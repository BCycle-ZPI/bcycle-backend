using bcycle_backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bcycle_backend.Data {
    public class BCycleContext : DbContext {
        public BCycleContext(DbContextOptions<BCycleContext> options) : base(options) {
        }

        public DbSet<GroupTrip> GroupTrips { get; set; }
        public DbSet<GroupTripParticipant> GroupTripParticipants { get; set; }
        public DbSet<GroupTripPoint> GroupTripPoints { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<TripPhoto> TripPhotos { get; set; }
        public DbSet<TripPoint> TripPoints { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            /*modelBuilder.Entity<GroupTrip>()
                .HasOne(e => e.StartPoint)
                .WithOne()
                .HasForeignKey<GroupTripPoint>(e => e.GroupTripID);
            modelBuilder.Entity<GroupTrip>()
                .HasOne(e => e.EndPoint)
                .WithOne()
                .HasForeignKey<GroupTripPoint>(e => e.GroupTripID);*/
            modelBuilder.Entity<GroupTrip>()
                .HasMany(e => e.GroupTripPoints)
                .WithOne().HasForeignKey(tp => tp.GroupTripID);

            /*modelBuilder.Entity<Trip>()
                .HasOne(e => e.StartPoint)
                .WithOne()
                ;//.HasForeignKey<TripPoint>(e => e.TripID);
            modelBuilder.Entity<Trip>()
                .HasOne(e => e.EndPoint)
                .WithOne()
                .HasForeignKey<TripPoint>(e => e.TripID);*/
            modelBuilder.Entity<Trip>()
                .HasMany(e => e.TripPoints)
                .WithOne().HasForeignKey(tp => tp.TripID);


            modelBuilder.Entity<GroupTrip>().ToTable("GroupTrip");
            modelBuilder.Entity<GroupTripParticipant>().ToTable("GroupTripParticipant");
            modelBuilder.Entity<GroupTripPoint>().ToTable("GroupTripPoint");
            modelBuilder.Entity<Trip>().ToTable("Trip");
            modelBuilder.Entity<TripPhoto>().ToTable("TripPhoto");
            modelBuilder.Entity<TripPoint>().ToTable("TripPoint");
            modelBuilder.Entity<User>().ToTable("User");
        }
    }
}
