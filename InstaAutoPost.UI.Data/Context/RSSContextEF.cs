using InstaAutoPost.UI.Data.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Data.Context
{
    public class RSSContextEF : DbContext
    {
        public DbSet<SourceContentImage> Images { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<SourceContent> SourceContents { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Email> Email { get; set; }
        public DbSet<EmailAccountOptions> EmailAccountOptions { get; set; }
        public DbSet<EmailOptions> EmailOptions { get; set; }
        public DbSet<AutoJob> AutoJob { get; set; }
        public DbSet<CategoryType> CategoryType { get; set; }
        public DbSet<SocialMediaAccountsCategoryType> SocialMediaAccountsCategoryType { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=TCBLGADMCONS023;Initial Catalog=InstaAutoPostRssDb;Integrated Security=SSPI;");
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SocialMediaAccountsCategoryType>()
    .HasKey(bc => new { bc.SocialMediaAccountId, bc.CategoryTypeId });
            modelBuilder.Entity<SocialMediaAccountsCategoryType>()
                .HasOne(bc => bc.SocialMediaAccounts)
                .WithMany(b => b.SocialMediaAccountsCategoryType)
                .HasForeignKey(bc => bc.SocialMediaAccountId);
            modelBuilder.Entity<SocialMediaAccountsCategoryType>()
                .HasOne(bc => bc.CategoryType)
                .WithMany(c => c.SocialMediaAccountsCategoryTypes)
                .HasForeignKey(bc => bc.CategoryTypeId);
            modelBuilder.Entity<SocialMediaAccountsCategoryType>()
                .HasKey(c => new { c.Id });


            modelBuilder.Entity<AutoJob>().HasData(
       new AutoJob
       {
           Id = 1,
           InsertedAt = DateTime.Now,
           UpdatedAt = DateTime.Now,
           Cron = null,
           IsWork = false,
           CronDescription = null,
           IsDeleted = false,
           JobTitle = "Otomatik Klasör Oluştur",
           JobName = "CreateFolder",
           JobDescription = "Belirli periyotlarla dosya oluşturan iş süreci",
           JobTimeType = "LongTime"
       }
   );
            modelBuilder.Entity<AutoJob>().HasData(
   new AutoJob
   {
       Id = 2,
       InsertedAt = DateTime.Now,
       UpdatedAt = DateTime.Now,
       Cron = null,
       IsWork = false,
       CronDescription = null,
       IsDeleted = false,
       JobTitle = "Otomatik İçerik Paylaş",
       JobName = "PublishContent",
       JobDescription = "Belirli periyotlarla içerik paylaşan iş süreci",
       JobTimeType = "ShortTime"
   }
);
            modelBuilder.Entity<AutoJob>().HasData(
new AutoJob
{
    Id = 3,
    InsertedAt = DateTime.Now,
    UpdatedAt = DateTime.Now,
    Cron = null,
    IsWork = false,
    CronDescription = null,
    IsDeleted = false,
    JobTitle = "Otomatik Yeni İçerik Ekle",
    JobName = "PullRSSContent",
    JobDescription = "Belirli periyotlarla eklenen RSS linklerinden yeni içerikleri çeken içerik süreci",
    JobTimeType = "ShortTime"
}
);
            modelBuilder.Entity<AutoJob>().HasData(
new AutoJob
{
    Id = 4,
    InsertedAt = DateTime.Now,
    UpdatedAt = DateTime.Now,
    Cron = null,
    IsWork = false,
    CronDescription = null,
    IsDeleted = false,
    JobTitle = "Otomatik Paylaşılan İçerikleri Sil",
    JobName = "RemoveContentPublished",
    JobDescription = "Belirli periyotlarla daha önce paylaşılan içerikleri uygulamadan silen iş süreci",
    JobTimeType = "LongTime"
}
);

            modelBuilder.Entity<AutoJob>().HasData(
new AutoJob
{
    Id=5,
    InsertedAt = DateTime.Now,
    UpdatedAt = DateTime.Now,
    Cron = null,
    IsWork = false,
    CronDescription = null,
    IsDeleted = false,
    JobTitle = "Otomatik Klasörlenen İçerikleri Sil",
    JobName = "RemoveContentCreatedFolder",
    JobDescription = "Belirli periyotlarla daha önce klasörlenen içerikleri uygulamadan kaldıran iş süreci",
    JobTimeType = "LongTime"
}
);
            modelBuilder.Entity<AutoJob>().HasData(
new AutoJob
{
    Id=6,
    InsertedAt = DateTime.Now,
    UpdatedAt = DateTime.Now,
    Cron = null,
    IsWork = false,
    CronDescription = null,
    IsDeleted = false,
    JobTitle = "Otomatik Gönderilen Mailleri Sil",
    JobName = "RemoveSentMails",
    JobDescription = "Belirli periyotlarla daha önce gönderilen mailleri uygulamadan kaldıran iş süreci",
    JobTimeType = "LongTime"
}
);

        }

    }
}
