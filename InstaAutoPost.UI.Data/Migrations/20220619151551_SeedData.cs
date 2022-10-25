using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InstaAutoPost.UI.Data.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AutoJob",
                columns: new[] { "Id", "Cron", "CronDescription", "InsertedAt", "IsDeleted", "IsWork", "JobDescription", "JobName", "JobTimeType", "JobTitle", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "0 0 * * 0", "Haftada 1 Kez", new DateTime(2022, 6, 19, 18, 15, 50, 201, DateTimeKind.Local).AddTicks(2669), false, false, "Belirli periyotlarla dosya oluşturan iş süreci", "CreateFolder", "LongTime", "Otomatik Klasör Oluştur", new DateTime(2022, 6, 19, 18, 15, 50, 205, DateTimeKind.Local).AddTicks(9872) },
                    { 2, "0 20 * * *", "Günde 1 Kez", new DateTime(2022, 6, 19, 18, 15, 50, 207, DateTimeKind.Local).AddTicks(3885), false, false, "Belirli periyotlarla içerik paylaşan iş süreci", "PublishContent", "ShortTime", "Otomatik İçerik Paylaş", new DateTime(2022, 6, 19, 18, 15, 50, 207, DateTimeKind.Local).AddTicks(3910) },
                    { 3, "0 20 * * *", "Günde 1 Kez", new DateTime(2022, 6, 19, 18, 15, 50, 207, DateTimeKind.Local).AddTicks(4067), false, false, "Belirli periyotlarla eklenen RSS linklerinden yeni içerikleri çeken içerik süreci", "PullRSSContent", "ShortTime", "Otomatik Yeni İçerik Ekle", new DateTime(2022, 6, 19, 18, 15, 50, 207, DateTimeKind.Local).AddTicks(4069) },
                    { 4, "0 0 * * 0", "Haftada 1 Kez", new DateTime(2022, 6, 19, 18, 15, 50, 207, DateTimeKind.Local).AddTicks(4091), false, false, "Belirli periyotlarla daha önce paylaşılan içerikleri uygulamadan silen iş süreci", "RemoveContentPublished", "LongTime", "Otomatik Paylaşılan İçerikleri Sil", new DateTime(2022, 6, 19, 18, 15, 50, 207, DateTimeKind.Local).AddTicks(4092) },
                    { 5, "0 0 * * 0", "Haftada 1 Kez", new DateTime(2022, 6, 19, 18, 15, 50, 207, DateTimeKind.Local).AddTicks(4111), false, false, "Belirli periyotlarla daha önce klasörlenen içerikleri uygulamadan silen iş süreci", "RemoveContentCreatedFolder", "LongTime", "Otomatik Klasörlenen İçerikleri Sil", new DateTime(2022, 6, 19, 18, 15, 50, 207, DateTimeKind.Local).AddTicks(4112) },
                    { 6, "0 0 * * 0", "Haftada 1 Kez", new DateTime(2022, 6, 19, 18, 15, 50, 207, DateTimeKind.Local).AddTicks(4136), false, false, "Belirli periyotlarla daha önce gönderilen mailleri uygulamadan silen iş süreci", "RemoveSentMails", "LongTime", "Otomatik Gönderilen Mailleri Sil", new DateTime(2022, 6, 19, 18, 15, 50, 207, DateTimeKind.Local).AddTicks(4137) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AutoJob",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AutoJob",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AutoJob",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AutoJob",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AutoJob",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AutoJob",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
