using Microsoft.EntityFrameworkCore.Migrations;

namespace InstaAutoPost.UI.Data.Migrations
{
    public partial class TagsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tags",
                table: "Categories");
        }
    }
}
