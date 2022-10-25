using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InstaAutoPost.UI.Data.Migrations
{
    public partial class lastMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AutoJob",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "InsertedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 6, 26, 2, 25, 42, 926, DateTimeKind.Local).AddTicks(1588), new DateTime(2022, 6, 26, 2, 25, 42, 933, DateTimeKind.Local).AddTicks(861) });

            migrationBuilder.UpdateData(
                table: "AutoJob",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "InsertedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 6, 26, 2, 25, 42, 936, DateTimeKind.Local).AddTicks(51), new DateTime(2022, 6, 26, 2, 25, 42, 936, DateTimeKind.Local).AddTicks(138) });

            migrationBuilder.UpdateData(
                table: "AutoJob",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "InsertedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 6, 26, 2, 25, 42, 936, DateTimeKind.Local).AddTicks(524), new DateTime(2022, 6, 26, 2, 25, 42, 936, DateTimeKind.Local).AddTicks(528) });

            migrationBuilder.UpdateData(
                table: "AutoJob",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "InsertedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 6, 26, 2, 25, 42, 936, DateTimeKind.Local).AddTicks(567), new DateTime(2022, 6, 26, 2, 25, 42, 936, DateTimeKind.Local).AddTicks(569) });

            migrationBuilder.UpdateData(
                table: "AutoJob",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "InsertedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 6, 26, 2, 25, 42, 936, DateTimeKind.Local).AddTicks(602), new DateTime(2022, 6, 26, 2, 25, 42, 936, DateTimeKind.Local).AddTicks(604) });

            migrationBuilder.UpdateData(
                table: "AutoJob",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "InsertedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 6, 26, 2, 25, 42, 936, DateTimeKind.Local).AddTicks(743), new DateTime(2022, 6, 26, 2, 25, 42, 936, DateTimeKind.Local).AddTicks(745) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "InsertedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 6, 25, 19, 11, 57, 506, DateTimeKind.Local).AddTicks(506), new DateTime(2022, 6, 25, 19, 11, 57, 506, DateTimeKind.Local).AddTicks(509) });

            migrationBuilder.UpdateData(
                table: "AutoJob",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "InsertedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 6, 25, 19, 11, 57, 506, DateTimeKind.Local).AddTicks(567), new DateTime(2022, 6, 25, 19, 11, 57, 506, DateTimeKind.Local).AddTicks(570) });
        }
    }
}
