using Microsoft.EntityFrameworkCore.Migrations;

namespace SmallPortal.Data.Migrations
{
    public partial class AddedIntuitId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IntuitId",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IntuitId",
                table: "AspNetUsers");
        }
    }
}
