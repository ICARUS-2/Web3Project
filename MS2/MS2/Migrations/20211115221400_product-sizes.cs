using Microsoft.EntityFrameworkCore.Migrations;

namespace MS2.Migrations
{
    public partial class productsizes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Products",
                newName: "SmallPrice");

            migrationBuilder.AddColumn<double>(
                name: "LargePrice",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MediumPrice",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "OrderEntry",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LargePrice",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MediumPrice",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "OrderEntry");

            migrationBuilder.RenameColumn(
                name: "SmallPrice",
                table: "Products",
                newName: "Price");
        }
    }
}
