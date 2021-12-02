using Microsoft.EntityFrameworkCore.Migrations;

namespace MS2.Migrations.Auth
{
    public partial class tsNameChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimeStamp",
                schema: "Identity",
                table: "AspNetUsers",
                newName: "CreatedAt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                schema: "Identity",
                table: "AspNetUsers",
                newName: "TimeStamp");
        }
    }
}
