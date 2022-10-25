using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InstaAutoPost.UI.Data.Migrations
{
    public partial class SeedEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AutoJob",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Cron", "CronDescription", "InsertedAt", "UpdatedAt" },
                values: new object[] { null, null, new DateTime(2022, 6, 19, 18, 20, 51, 659, DateTimeKind.Local).AddTicks(435), new DateTime(2022, 6, 19, 18, 20, 51, 664, DateTimeKind.Local).AddTicks(1387) });

            migrationBuilder.UpdateData(
                table: "AutoJob",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Cron", "CronDescription", "InsertedAt", "UpdatedAt" },
                values: new object[] { null, null, new DateTime(2022, 6, 19, 18, 20, 51, 665, DateTimeKind.Local).AddTicks(3545), new DateTime(2022, 6, 19, 18, 20, 51, 665, DateTimeKind.Local).AddTicks(3580) });

            migrationBuilder.UpdateData(
                table: "AutoJob",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Cron", "CronDescription", "InsertedAt", "UpdatedAt" },
                values: new object[] { null, null, new DateTime(2022, 6, 19, 18, 20, 51, 665, DateTimeKind.Local).AddTicks(3753), new DateTime(2022, 6, 19, 18, 20, 51, 665, DateTimeKind.Local).AddTicks(3755) });

            migrationBuilder.UpdateData(
                table: "AutoJob",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Cron", "CronDescription", "InsertedAt", "UpdatedAt" },
                values: new object[] { null, null, new DateTime(2022, 6, 19, 18, 20, 51, 665, DateTimeKind.Local).AddTicks(3776), new DateTime(2022, 6, 19, 18, 20, 51, 665, DateTimeKind.Local).AddTicks(3777) });

            migrationBuilder.UpdateData(
                table: "AutoJob",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Cron", "CronDescription", "InsertedAt", "UpdatedAt" },
                values: new object[] { null, null, new DateTime(2022, 6, 19, 18, 20, 51, 665, DateTimeKind.Local).AddTicks(3841), new DateTime(2022, 6, 19, 18, 20, 51, 665, DateTimeKind.Local).AddTicks(3843) });

            migrationBuilder.UpdateData(
                table: "AutoJob",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Cron", "CronDescription", "InsertedAt", "UpdatedAt" },
                values: new object[] { null, null, new DateTime(2022, 6, 19, 18, 20, 51, 665, DateTimeKind.Local).AddTicks(3869), new DateTime(2022, 6, 19, 18, 20, 51, 665, DateTimeKind.Local).AddTicks(3871) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AutoJob",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Cron", "CronDescription", "InsertedAt", "UpdatedAt" },
                values: new object[] { "0 0 * * 0", "Haftada 1 Kez", new DateTime(2022, 6, 19, 18, 15, 50, 201, DateTimeKind.Local).AddTicks(2669), new DateTime(2022, 6, 19, 18, 15, 50, 205, DateTimeKind.Local).AddTicks(9872) });

            migrationBuilder.UpdateData(
                table: "AutoJob",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Cron", "CronDescription", "InsertedAt", "UpdatedAt" },
                values: new object[] { "0 20 * * *", "Günde 1 Kez", new DateTime(2022, 6, 19, 18, 15, 50, 207, DateTimeKind.Local).AddTicks(3885), new DateTime(2022, 6, 19, 18, 15, 50, 207, DateTimeKind.Local).AddTicks(3910) });

            migrationBuilder.UpdateData(
                table: "AutoJob",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Cron", "CronDescription", "InsertedAt", "UpdatedAt" },
                values: new object[] { "0 20 * * *", "Günde 1 Kez", new DateTime(2022, 6, 19, 18, 15, 50, 207, DateTimeKind.Local).AddTicks(4067), new DateTime(2022, 6, 19, 18, 15, 50, 207, DateTimeKind.Local).AddTicks(4069) });

            migrationBuilder.UpdateData(
                table: "AutoJob",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Cron", "CronDescription", "InsertedAt", "UpdatedAt" },
                values: new object[] { "0 0 * * 0", "Haftada 1 Kez", new DateTime(2022, 6, 19, 18, 15, 50, 207, DateTimeKind.Local).AddTicks(4091), new DateTime(2022, 6, 19, 18, 15, 50, 207, DateTimeKind.Local).AddTicks(4092) });

            migrationBuilder.UpdateData(
                table: "AutoJob",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Cron", "CronDescription", "InsertedAt", "UpdatedAt" },
                values: new object[] { "0 0 * * 0", "Haftada 1 Kez", new DateTime(2022, 6, 19, 18, 15, 50, 207, DateTimeKind.Local).AddTicks(4111), new DateTime(2022, 6, 19, 18, 15, 50, 207, DateTimeKind.Local).AddTicks(4112) });

            migrationBuilder.UpdateData(
                table: "AutoJob",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Cron", "CronDescription", "InsertedAt", "UpdatedAt" },
                values: new object[] { "0 0 * * 0", "Haftada 1 Kez", new DateTime(2022, 6, 19, 18, 15, 50, 207, DateTimeKind.Local).AddTicks(4136), new DateTime(2022, 6, 19, 18, 15, 50, 207, DateTimeKind.Local).AddTicks(4137) });
        }
    }
}
