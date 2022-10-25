using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InstaAutoPost.UI.Data.Migrations
{
    public partial class TemplateAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Template",
                table: "CategoryType",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AutoJob",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "InsertedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 6, 25, 19, 11, 57, 498, DateTimeKind.Local).AddTicks(4445), new DateTime(2022, 6, 25, 19, 11, 57, 503, DateTimeKind.Local).AddTicks(7013) });

            migrationBuilder.UpdateData(
                table: "AutoJob",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "InsertedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 6, 25, 19, 11, 57, 505, DateTimeKind.Local).AddTicks(9966), new DateTime(2022, 6, 25, 19, 11, 57, 506, DateTimeKind.Local).AddTicks(15) });

            migrationBuilder.UpdateData(
                table: "AutoJob",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "InsertedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 6, 25, 19, 11, 57, 506, DateTimeKind.Local).AddTicks(337), new DateTime(2022, 6, 25, 19, 11, 57, 506, DateTimeKind.Local).AddTicks(342) });

            migrationBuilder.UpdateData(
                table: "AutoJob",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "InsertedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 6, 25, 19, 11, 57, 506, DateTimeKind.Local).AddTicks(392), new DateTime(2022, 6, 25, 19, 11, 57, 506, DateTimeKind.Local).AddTicks(396) });

            migrationBuilder.UpdateData(
                table: "AutoJob",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "InsertedAt", "JobDescription", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 6, 25, 19, 11, 57, 506, DateTimeKind.Local).AddTicks(506), "Belirli periyotlarla daha önce klasörlenen içerikleri uygulamadan kaldıran iş süreci", new DateTime(2022, 6, 25, 19, 11, 57, 506, DateTimeKind.Local).AddTicks(509) });

            migrationBuilder.UpdateData(
                table: "AutoJob",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "InsertedAt", "JobDescription", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 6, 25, 19, 11, 57, 506, DateTimeKind.Local).AddTicks(567), "Belirli periyotlarla daha önce gönderilen mailleri uygulamadan kaldıran iş süreci", new DateTime(2022, 6, 25, 19, 11, 57, 506, DateTimeKind.Local).AddTicks(570) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Template",
                table: "CategoryType");

            migrationBuilder.UpdateData(
                table: "AutoJob",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "InsertedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 6, 19, 18, 20, 51, 659, DateTimeKind.Local).AddTicks(435), new DateTime(2022, 6, 19, 18, 20, 51, 664, DateTimeKind.Local).AddTicks(1387) });

            migrationBuilder.UpdateData(
                table: "AutoJob",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "InsertedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 6, 19, 18, 20, 51, 665, DateTimeKind.Local).AddTicks(3545), new DateTime(2022, 6, 19, 18, 20, 51, 665, DateTimeKind.Local).AddTicks(3580) });

            migrationBuilder.UpdateData(
                table: "AutoJob",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "InsertedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 6, 19, 18, 20, 51, 665, DateTimeKind.Local).AddTicks(3753), new DateTime(2022, 6, 19, 18, 20, 51, 665, DateTimeKind.Local).AddTicks(3755) });

            migrationBuilder.UpdateData(
                table: "AutoJob",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "InsertedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 6, 19, 18, 20, 51, 665, DateTimeKind.Local).AddTicks(3776), new DateTime(2022, 6, 19, 18, 20, 51, 665, DateTimeKind.Local).AddTicks(3777) });

            migrationBuilder.UpdateData(
                table: "AutoJob",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "InsertedAt", "JobDescription", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 6, 19, 18, 20, 51, 665, DateTimeKind.Local).AddTicks(3841), "Belirli periyotlarla daha önce klasörlenen içerikleri uygulamadan silen iş süreci", new DateTime(2022, 6, 19, 18, 20, 51, 665, DateTimeKind.Local).AddTicks(3843) });

            migrationBuilder.UpdateData(
                table: "AutoJob",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "InsertedAt", "JobDescription", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 6, 19, 18, 20, 51, 665, DateTimeKind.Local).AddTicks(3869), "Belirli periyotlarla daha önce gönderilen mailleri uygulamadan silen iş süreci", new DateTime(2022, 6, 19, 18, 20, 51, 665, DateTimeKind.Local).AddTicks(3871) });
        }
    }
}
