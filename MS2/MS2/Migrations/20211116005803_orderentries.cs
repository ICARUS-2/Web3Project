using Microsoft.EntityFrameworkCore.Migrations;

namespace MS2.Migrations
{
    public partial class orderentries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderEntry_Orders_OrderNumber",
                table: "OrderEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderEntry_Products_ProductId",
                table: "OrderEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderEntry",
                table: "OrderEntry");

            migrationBuilder.RenameTable(
                name: "OrderEntry",
                newName: "OrderEntries");

            migrationBuilder.RenameIndex(
                name: "IX_OrderEntry_ProductId",
                table: "OrderEntries",
                newName: "IX_OrderEntries_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderEntry_OrderNumber",
                table: "OrderEntries",
                newName: "IX_OrderEntries_OrderNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderEntries",
                table: "OrderEntries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderEntries_Orders_OrderNumber",
                table: "OrderEntries",
                column: "OrderNumber",
                principalTable: "Orders",
                principalColumn: "OrderNumber",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderEntries_Products_ProductId",
                table: "OrderEntries",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderEntries_Orders_OrderNumber",
                table: "OrderEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderEntries_Products_ProductId",
                table: "OrderEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderEntries",
                table: "OrderEntries");

            migrationBuilder.RenameTable(
                name: "OrderEntries",
                newName: "OrderEntry");

            migrationBuilder.RenameIndex(
                name: "IX_OrderEntries_ProductId",
                table: "OrderEntry",
                newName: "IX_OrderEntry_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderEntries_OrderNumber",
                table: "OrderEntry",
                newName: "IX_OrderEntry_OrderNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderEntry",
                table: "OrderEntry",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderEntry_Orders_OrderNumber",
                table: "OrderEntry",
                column: "OrderNumber",
                principalTable: "Orders",
                principalColumn: "OrderNumber",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderEntry_Products_ProductId",
                table: "OrderEntry",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
