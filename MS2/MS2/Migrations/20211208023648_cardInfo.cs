using Microsoft.EntityFrameworkCore.Migrations;

namespace MS2.Migrations
{
    public partial class cardInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CardInfo",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardInfo",
                table: "Orders");
        }
    }
}
