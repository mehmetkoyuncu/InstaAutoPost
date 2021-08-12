using Microsoft.EntityFrameworkCore.Migrations;

namespace InstaAutoPost.UI.Data.Migrations
{
    public partial class entitiesChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SourceContentId",
                table: "SourceContents",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SourceContentId",
                table: "SourceContents");
        }
    }
}
