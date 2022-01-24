using Microsoft.EntityFrameworkCore.Migrations;

namespace InstaAutoPost.UI.Data.Migrations
{
    public partial class aa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailOptions_EmailAccountOptions_EmailAccountOptionsId",
                table: "EmailOptions");

            migrationBuilder.DropIndex(
                name: "IX_EmailOptions_EmailAccountOptionsId",
                table: "EmailOptions");

            migrationBuilder.DropColumn(
                name: "EmailAccountOptionsId",
                table: "EmailOptions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmailAccountOptionsId",
                table: "EmailOptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EmailOptions_EmailAccountOptionsId",
                table: "EmailOptions",
                column: "EmailAccountOptionsId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailOptions_EmailAccountOptions_EmailAccountOptionsId",
                table: "EmailOptions",
                column: "EmailAccountOptionsId",
                principalTable: "EmailAccountOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
