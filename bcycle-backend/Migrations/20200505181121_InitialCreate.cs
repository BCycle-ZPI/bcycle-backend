using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace bcycle_backend.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroupTrips",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    HostId = table.Column<string>(nullable: true),
                    TripCode = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    SharingGuid = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTrips", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupTripParticipants",
                columns: table => new
                {
                    GroupTripId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTripParticipants", x => new { x.UserId, x.GroupTripId });
                    table.ForeignKey(
                        name: "FK_GroupTripParticipants_GroupTrips_GroupTripId",
                        column: x => x.GroupTripId,
                        principalTable: "GroupTrips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupTripPoints",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GroupTripId = table.Column<int>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Ordinal = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTripPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupTripPoints_GroupTrips_GroupTripId",
                        column: x => x.GroupTripId,
                        principalTable: "GroupTrips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Distance = table.Column<double>(nullable: false),
                    Time = table.Column<int>(nullable: false),
                    Started = table.Column<DateTime>(nullable: false),
                    Finished = table.Column<DateTime>(nullable: false),
                    SharingGuid = table.Column<Guid>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    GroupTripId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trips_GroupTrips_GroupTripId",
                        column: x => x.GroupTripId,
                        principalTable: "GroupTrips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TripPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PhotoUrl = table.Column<string>(nullable: true),
                    TripId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TripPhotos_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TripPoints",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TripId = table.Column<int>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    TimeReached = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TripPoints_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupTripParticipants_GroupTripId",
                table: "GroupTripParticipants",
                column: "GroupTripId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupTripPoints_GroupTripId",
                table: "GroupTripPoints",
                column: "GroupTripId");

            migrationBuilder.CreateIndex(
                name: "IX_TripPhotos_TripId",
                table: "TripPhotos",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_TripPoints_TripId",
                table: "TripPoints",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_GroupTripId",
                table: "Trips",
                column: "GroupTripId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupTripParticipants");

            migrationBuilder.DropTable(
                name: "GroupTripPoints");

            migrationBuilder.DropTable(
                name: "TripPhotos");

            migrationBuilder.DropTable(
                name: "TripPoints");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "GroupTrips");
        }
    }
}
