using Microsoft.EntityFrameworkCore.Migrations;

namespace InstaAutoPost.UI.Data.Migrations
{
    public partial class addSourceURL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "URL",
                table: "Sources",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "URL",
                table: "Sources");
        }
    }
}
