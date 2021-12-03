using Microsoft.EntityFrameworkCore.Migrations;

namespace MS2.Migrations
{
    public partial class orderDriverId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeliveryDriverId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryDriverId",
                table: "Orders");
        }
    }
}
