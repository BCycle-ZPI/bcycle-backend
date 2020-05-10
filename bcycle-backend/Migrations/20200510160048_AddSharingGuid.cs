using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace bcycle_backend.Migrations
{
    public partial class AddSharingGuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SharingGuid",
                table: "Trips",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SharingGuid",
                table: "GroupTrips",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SharingGuid",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "SharingGuid",
                table: "GroupTrips");
        }
    }
}
