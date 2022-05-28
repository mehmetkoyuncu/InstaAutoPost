using Microsoft.EntityFrameworkCore.Migrations;

namespace InstaAutoPost.UI.Data.Migrations
{
    public partial class addedSpecialPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSpecialPost",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSpecialPost",
                table: "Posts");
        }
    }
}
