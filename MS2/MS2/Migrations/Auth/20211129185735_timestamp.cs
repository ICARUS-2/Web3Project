using Microsoft.EntityFrameworkCore.Migrations;

namespace MS2.Migrations.Auth
{
    public partial class timestamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TimeStamp",
                schema: "Identity",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeStamp",
                schema: "Identity",
                table: "AspNetUsers");
        }
    }
}
