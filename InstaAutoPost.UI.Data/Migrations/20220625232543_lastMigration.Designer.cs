﻿// <auto-generated />
using System;
using InstaAutoPost.UI.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InstaAutoPost.UI.Data.Migrations
{
    [DbContext(typeof(RSSContextEF))]
    [Migration("20220625232543_lastMigration")]
    partial class lastMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("InstaAutoPost.UI.Data.Entities.Concrete.AutoJob", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cron")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CronDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("InsertedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsWork")
                        .HasColumnType("bit");

                    b.Property<string>("JobDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JobName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JobTimeType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JobTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("AutoJob");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            InsertedAt = new DateTime(2022, 6, 26, 2, 25, 42, 926, DateTimeKind.Local).AddTicks(1588),
                            IsDeleted = false,
                            IsWork = false,
                            JobDescription = "Belirli periyotlarla dosya oluşturan iş süreci",
                            JobName = "CreateFolder",
                            JobTimeType = "LongTime",
                            JobTitle = "Otomatik Klasör Oluştur",
                            UpdatedAt = new DateTime(2022, 6, 26, 2, 25, 42, 933, DateTimeKind.Local).AddTicks(861)
                        },
                        new
                        {
                            Id = 2,
                            InsertedAt = new DateTime(2022, 6, 26, 2, 25, 42, 936, DateTimeKind.Local).AddTicks(51),
                            IsDeleted = false,
                            IsWork = false,
                            JobDescription = "Belirli periyotlarla içerik paylaşan iş süreci",
                            JobName = "PublishContent",
                            JobTimeType = "ShortTime",
                            JobTitle = "Otomatik İçerik Paylaş",
                            UpdatedAt = new DateTime(2022, 6, 26, 2, 25, 42, 936, DateTimeKind.Local).AddTicks(138)
                        },
                        new
                        {
                            Id = 3,
                            InsertedAt = new DateTime(2022, 6, 26, 2, 25, 42, 936, DateTimeKind.Local).AddTicks(524),
                            IsDeleted = false,
                            IsWork = false,
                            JobDescription = "Belirli periyotlarla eklenen RSS linklerinden yeni içerikleri çeken içerik süreci",
                            JobName = "PullRSSContent",
                            JobTimeType = "ShortTime",
                            JobTitle = "Otomatik Yeni İçerik Ekle",
                            UpdatedAt = new DateTime(2022, 6, 26, 2, 25, 42, 936, DateTimeKind.Local).AddTicks(528)
                        },
                        new
                        {
                            Id = 4,
                            InsertedAt = new DateTime(2022, 6, 26, 2, 25, 42, 936, DateTimeKind.Local).AddTicks(567),
                            IsDeleted = false,
                            IsWork = false,
                            JobDescription = "Belirli periyotlarla daha önce paylaşılan içerikleri uygulamadan silen iş süreci",
                            JobName = "RemoveContentPublished",
                            JobTimeType = "LongTime",
                            JobTitle = "Otomatik Paylaşılan İçerikleri Sil",
                            UpdatedAt = new DateTime(2022, 6, 26, 2, 25, 42, 936, DateTimeKind.Local).AddTicks(569)
                        },
                        new
                        {
                            Id = 5,
                            InsertedAt = new DateTime(2022, 6, 26, 2, 25, 42, 936, DateTimeKind.Local).AddTicks(602),
                            IsDeleted = false,
                            IsWork = false,
                            JobDescription = "Belirli periyotlarla daha önce klasörlenen içerikleri uygulamadan kaldıran iş süreci",
                            JobName = "RemoveContentCreatedFolder",
                            JobTimeType = "LongTime",
                            JobTitle = "Otomatik Klasörlenen İçerikleri Sil",
                            UpdatedAt = new DateTime(2022, 6, 26, 2, 25, 42, 936, DateTimeKind.Local).AddTicks(604)
                        },
                        new
                        {
                            Id = 6,
                            InsertedAt = new DateTime(2022, 6, 26, 2, 25, 42, 936, DateTimeKind.Local).AddTicks(743),
                            IsDeleted = false,
                            IsWork = false,
                            JobDescription = "Belirli periyotlarla daha önce gönderilen mailleri uygulamadan kaldıran iş süreci",
                            JobName = "RemoveSentMails",
                            JobTimeType = "LongTime",
                            JobTitle = "Otomatik Gönderilen Mailleri Sil",
                            UpdatedAt = new DateTime(2022, 6, 26, 2, 25, 42, 936, DateTimeKind.Local).AddTicks(745)
                        });
                });

            modelBuilder.Entity("InstaAutoPost.UI.Data.Entities.Concrete.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("InsertedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SourceId")
                        .HasColumnType("int");

                    b.Property<string>("Tags")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CategoryTypeId");

                    b.HasIndex("SourceId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("InstaAutoPost.UI.Data.Entities.Concrete.CategoryType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("InsertedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Template")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("CategoryType");
                });

            modelBuilder.Entity("InstaAutoPost.UI.Data.Entities.Concrete.Email", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Body")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ErrorText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("From")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HtmlBody")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("InsertedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSuccess")
                        .HasColumnType("bit");

                    b.Property<DateTime>("SendDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Subject")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("To")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Email");
                });

            modelBuilder.Entity("InstaAutoPost.UI.Data.Entities.Concrete.EmailAccountOptions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountMailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AccountMailPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("InsertedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("EmailAccountOptions");
                });

            modelBuilder.Entity("InstaAutoPost.UI.Data.Entities.Concrete.EmailOptions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("InsertedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("MailDefaultContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MailDefaultHTMLContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MailDefaultSubject")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MailDefaultTo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("EmailOptions");
                });

            modelBuilder.Entity("InstaAutoPost.UI.Data.Entities.Concrete.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ContentId")
                        .HasColumnType("int");

                    b.Property<DateTime>("InsertedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSpecialPost")
                        .HasColumnType("bit");

                    b.Property<int>("OrderNumber")
                        .HasColumnType("int");

                    b.Property<int>("SocialMediaAccountsCategoryTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("SocialMediaAccountsCategoryTypeId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("InstaAutoPost.UI.Data.Entities.Concrete.SocialMediaAccounts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountNameOrMail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Icon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("InsertedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("SocialMediaAccounts");
                });

            modelBuilder.Entity("InstaAutoPost.UI.Data.Entities.Concrete.SocialMediaAccountsCategoryType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("InsertedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("SocialMediaAccountId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CategoryTypeId");

                    b.HasIndex("SocialMediaAccountId");

                    b.ToTable("SocialMediaAccountsCategoryType");
                });

            modelBuilder.Entity("InstaAutoPost.UI.Data.Entities.Concrete.Source", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("InsertedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("URL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Sources");
                });

            modelBuilder.Entity("InstaAutoPost.UI.Data.Entities.Concrete.SourceContent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ContentInsertAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("InsertedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsCreatedFolder")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("PostTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("SendOutForPost")
                        .HasColumnType("bit");

                    b.Property<string>("SourceContentId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tags")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("imageURL")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("SourceContents");
                });

            modelBuilder.Entity("InstaAutoPost.UI.Data.Entities.Concrete.SourceContentImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImageLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("InsertedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("SourceContentId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("SourceContentId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("InstaAutoPost.UI.Data.Entities.Concrete.Category", b =>
                {
                    b.HasOne("InstaAutoPost.UI.Data.Entities.Concrete.CategoryType", "CategoryType")
                        .WithMany("Category")
                        .HasForeignKey("CategoryTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InstaAutoPost.UI.Data.Entities.Concrete.Source", "Source")
                        .WithMany("Categories")
                        .HasForeignKey("SourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoryType");

                    b.Navigation("Source");
                });

            modelBuilder.Entity("InstaAutoPost.UI.Data.Entities.Concrete.Post", b =>
                {
                    b.HasOne("InstaAutoPost.UI.Data.Entities.Concrete.SocialMediaAccountsCategoryType", "SocialMediaAccountsCategoryType")
                        .WithMany("Posts")
                        .HasForeignKey("SocialMediaAccountsCategoryTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SocialMediaAccountsCategoryType");
                });

            modelBuilder.Entity("InstaAutoPost.UI.Data.Entities.Concrete.SocialMediaAccountsCategoryType", b =>
                {
                    b.HasOne("InstaAutoPost.UI.Data.Entities.Concrete.CategoryType", "CategoryType")
                        .WithMany("SocialMediaAccountsCategoryTypes")
                        .HasForeignKey("CategoryTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InstaAutoPost.UI.Data.Entities.Concrete.SocialMediaAccounts", "SocialMediaAccounts")
                        .WithMany("SocialMediaAccountsCategoryType")
                        .HasForeignKey("SocialMediaAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoryType");

                    b.Navigation("SocialMediaAccounts");
                });

            modelBuilder.Entity("InstaAutoPost.UI.Data.Entities.Concrete.SourceContent", b =>
                {
                    b.HasOne("InstaAutoPost.UI.Data.Entities.Concrete.Category", "Category")
                        .WithMany("SourceContents")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("InstaAutoPost.UI.Data.Entities.Concrete.SourceContentImage", b =>
                {
                    b.HasOne("InstaAutoPost.UI.Data.Entities.Concrete.SourceContent", "SourceContent")
                        .WithMany()
                        .HasForeignKey("SourceContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SourceContent");
                });

            modelBuilder.Entity("InstaAutoPost.UI.Data.Entities.Concrete.Category", b =>
                {
                    b.Navigation("SourceContents");
                });

            modelBuilder.Entity("InstaAutoPost.UI.Data.Entities.Concrete.CategoryType", b =>
                {
                    b.Navigation("Category");

                    b.Navigation("SocialMediaAccountsCategoryTypes");
                });

            modelBuilder.Entity("InstaAutoPost.UI.Data.Entities.Concrete.SocialMediaAccounts", b =>
                {
                    b.Navigation("SocialMediaAccountsCategoryType");
                });

            modelBuilder.Entity("InstaAutoPost.UI.Data.Entities.Concrete.SocialMediaAccountsCategoryType", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("InstaAutoPost.UI.Data.Entities.Concrete.Source", b =>
                {
                    b.Navigation("Categories");
                });
#pragma warning restore 612, 618
        }
    }
}
