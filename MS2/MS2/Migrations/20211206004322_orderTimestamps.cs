using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MS2.Migrations
{
    public partial class orderTimestamps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedTS",
                table: "OrderEntries",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PreparingTS",
                table: "OrderEntries",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedTS",
                table: "OrderEntries");

            migrationBuilder.DropColumn(
                name: "PreparingTS",
                table: "OrderEntries");
        }
    }
}
