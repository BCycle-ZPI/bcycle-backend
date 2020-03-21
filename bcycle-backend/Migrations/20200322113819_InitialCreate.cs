using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace bcycle_backend.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroupTrip",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    TripCode = table.Column<string>(nullable: true),
                    MapImageUrl = table.Column<string>(nullable: true),
                    HostId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTrip", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupTripParticipant",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsApproved = table.Column<bool>(nullable: false),
                    GroupTripId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTripParticipant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupTripParticipant_GroupTrip_GroupTripId",
                        column: x => x.GroupTripId,
                        principalTable: "GroupTrip",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupTripPoint",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Longitude = table.Column<float>(nullable: false),
                    Latitude = table.Column<float>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    GroupTripId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTripPoint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupTripPoint_GroupTrip_GroupTripId",
                        column: x => x.GroupTripId,
                        principalTable: "GroupTrip",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trip",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Distance = table.Column<double>(nullable: false),
                    Time = table.Column<int>(nullable: false),
                    Started = table.Column<DateTime>(nullable: false),
                    Finished = table.Column<DateTime>(nullable: false),
                    MapImageUrl = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    GroupTripId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trip", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trip_GroupTrip_GroupTripId",
                        column: x => x.GroupTripId,
                        principalTable: "GroupTrip",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TripPhoto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PhotoUrl = table.Column<string>(nullable: true),
                    TripId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripPhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TripPhoto_Trip_TripId",
                        column: x => x.TripId,
                        principalTable: "Trip",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TripPoint",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TripId = table.Column<int>(nullable: false),
                    Latitude = table.Column<float>(nullable: false),
                    Longitude = table.Column<float>(nullable: false),
                    TimeReached = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripPoint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TripPoint_Trip_TripId",
                        column: x => x.TripId,
                        principalTable: "Trip",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupTripParticipant_GroupTripId",
                table: "GroupTripParticipant",
                column: "GroupTripId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupTripPoint_GroupTripId",
                table: "GroupTripPoint",
                column: "GroupTripId");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_GroupTripId",
                table: "Trip",
                column: "GroupTripId");

            migrationBuilder.CreateIndex(
                name: "IX_TripPhoto_TripId",
                table: "TripPhoto",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_TripPoint_TripId",
                table: "TripPoint",
                column: "TripId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupTripParticipant");

            migrationBuilder.DropTable(
                name: "GroupTripPoint");

            migrationBuilder.DropTable(
                name: "TripPhoto");

            migrationBuilder.DropTable(
                name: "TripPoint");

            migrationBuilder.DropTable(
                name: "Trip");

            migrationBuilder.DropTable(
                name: "GroupTrip");
        }
    }
}
