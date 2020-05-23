using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace bcycle_backend.Migrations
{
    public partial class RemoveSharingGuidFromGroupTrips : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SharingGuid",
                table: "GroupTrips");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SharingGuid",
                table: "GroupTrips",
                nullable: true);
        }
    }
}
