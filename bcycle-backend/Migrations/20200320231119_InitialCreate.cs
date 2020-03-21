using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace bcycle_backend.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    PhotoUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GroupTrip",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    TripCode = table.Column<string>(nullable: true),
                    MapImageUrl = table.Column<string>(nullable: true),
                    HostID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTrip", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GroupTrip_User_HostID",
                        column: x => x.HostID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupTripParticipant",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsApproved = table.Column<bool>(nullable: false),
                    GroupTripID = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTripParticipant", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GroupTripParticipant_GroupTrip_GroupTripID",
                        column: x => x.GroupTripID,
                        principalTable: "GroupTrip",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupTripParticipant_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupTripPoint",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Longitude = table.Column<float>(nullable: false),
                    Latitude = table.Column<float>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    GroupTripID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTripPoint", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GroupTripPoint_GroupTrip_GroupTripID",
                        column: x => x.GroupTripID,
                        principalTable: "GroupTrip",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trip",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Distance = table.Column<double>(nullable: false),
                    Time = table.Column<int>(nullable: false),
                    Started = table.Column<DateTime>(nullable: false),
                    Finished = table.Column<DateTime>(nullable: false),
                    MapImageUrl = table.Column<string>(nullable: true),
                    UserID = table.Column<int>(nullable: false),
                    GroupTripID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trip", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Trip_GroupTrip_GroupTripID",
                        column: x => x.GroupTripID,
                        principalTable: "GroupTrip",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trip_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TripPhoto",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PhotoUrl = table.Column<string>(nullable: true),
                    TripID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripPhoto", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TripPhoto_Trip_TripID",
                        column: x => x.TripID,
                        principalTable: "Trip",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TripPoint",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TripID = table.Column<int>(nullable: false),
                    Latitude = table.Column<float>(nullable: false),
                    Longitude = table.Column<float>(nullable: false),
                    TimeReached = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripPoint", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TripPoint_Trip_TripID",
                        column: x => x.TripID,
                        principalTable: "Trip",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupTrip_HostID",
                table: "GroupTrip",
                column: "HostID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupTripParticipant_GroupTripID",
                table: "GroupTripParticipant",
                column: "GroupTripID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupTripParticipant_UserID",
                table: "GroupTripParticipant",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupTripPoint_GroupTripID",
                table: "GroupTripPoint",
                column: "GroupTripID");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_GroupTripID",
                table: "Trip",
                column: "GroupTripID");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_UserID",
                table: "Trip",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_TripPhoto_TripID",
                table: "TripPhoto",
                column: "TripID");

            migrationBuilder.CreateIndex(
                name: "IX_TripPoint_TripID",
                table: "TripPoint",
                column: "TripID");
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

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
