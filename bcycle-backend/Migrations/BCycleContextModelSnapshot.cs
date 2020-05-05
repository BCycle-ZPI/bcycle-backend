﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using bcycle_backend.Data;

namespace bcycle_backend.Migrations
{
    [DbContext(typeof(BCycleContext))]
    partial class BCycleContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("bcycle_backend.Models.Entities.GroupTrip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("HostId");

                    b.Property<string>("Name");

                    b.Property<Guid?>("SharingGuid");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("TripCode");

                    b.HasKey("Id");

                    b.ToTable("GroupTrips");
                });

            modelBuilder.Entity("bcycle_backend.Models.Entities.GroupTripParticipant", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<int>("GroupTripId");

                    b.Property<int>("Status");

                    b.HasKey("UserId", "GroupTripId");

                    b.HasIndex("GroupTripId");

                    b.ToTable("GroupTripParticipants");
                });

            modelBuilder.Entity("bcycle_backend.Models.Entities.GroupTripPoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GroupTripId");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<int>("Ordinal");

                    b.HasKey("Id");

                    b.HasIndex("GroupTripId");

                    b.ToTable("GroupTripPoints");
                });

            modelBuilder.Entity("bcycle_backend.Models.Entities.Trip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Distance");

                    b.Property<DateTime>("Finished");

                    b.Property<int?>("GroupTripId");

                    b.Property<Guid?>("SharingGuid");

                    b.Property<DateTime>("Started");

                    b.Property<int>("Time");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("GroupTripId");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("bcycle_backend.Models.Entities.TripPhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("PhotoUrl");

                    b.Property<int>("TripId");

                    b.HasKey("Id");

                    b.HasIndex("TripId");

                    b.ToTable("TripPhotos");
                });

            modelBuilder.Entity("bcycle_backend.Models.Entities.TripPoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<DateTime>("TimeReached");

                    b.Property<int>("TripId");

                    b.HasKey("Id");

                    b.HasIndex("TripId");

                    b.ToTable("TripPoints");
                });

            modelBuilder.Entity("bcycle_backend.Models.Entities.GroupTripParticipant", b =>
                {
                    b.HasOne("bcycle_backend.Models.Entities.GroupTrip", "GroupTrip")
                        .WithMany("Participants")
                        .HasForeignKey("GroupTripId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("bcycle_backend.Models.Entities.GroupTripPoint", b =>
                {
                    b.HasOne("bcycle_backend.Models.Entities.GroupTrip", "GroupTrip")
                        .WithMany("Route")
                        .HasForeignKey("GroupTripId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("bcycle_backend.Models.Entities.Trip", b =>
                {
                    b.HasOne("bcycle_backend.Models.Entities.GroupTrip", "GroupTrip")
                        .WithMany("Trips")
                        .HasForeignKey("GroupTripId");
                });

            modelBuilder.Entity("bcycle_backend.Models.Entities.TripPhoto", b =>
                {
                    b.HasOne("bcycle_backend.Models.Entities.Trip", "Trip")
                        .WithMany("Photos")
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("bcycle_backend.Models.Entities.TripPoint", b =>
                {
                    b.HasOne("bcycle_backend.Models.Entities.Trip", "Trip")
                        .WithMany("Route")
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
