﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NZWalks.API.Data;

#nullable disable

namespace NZWalks.API.Migrations
{
    [DbContext(typeof(NZWalksDbContext))]
    [Migration("20240828155328_Seeding data for Difficulties and Regions")]
    partial class SeedingdataforDifficultiesandRegions
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NZWalks.API.Models.Domain.Difficulty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Difficulties");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a28f34ab-9898-4a70-b057-fc2a98470aeb"),
                            Name = "Easy"
                        },
                        new
                        {
                            Id = new Guid("36f72fe7-43d6-4e3f-8934-d05b13a0e7de"),
                            Name = "Medium"
                        },
                        new
                        {
                            Id = new Guid("9ea58ba1-094a-4d5d-b8d9-39ce0ec0a3f4"),
                            Name = "Hard"
                        });
                });

            modelBuilder.Entity("NZWalks.API.Models.Domain.Region", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegionImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Regions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ac8acc08-94ff-47ff-6862-08dcc349a7bc"),
                            Code = "REG002",
                            Name = "Europe",
                            RegionImageUrl = "http://example.com/region/europe.png"
                        },
                        new
                        {
                            Id = new Guid("7d388ef1-33a0-478d-778e-08dcc5f1aee4"),
                            Code = "VN",
                            Name = "Vietnam",
                            RegionImageUrl = "https://example.com/images/vietnam.png"
                        },
                        new
                        {
                            Id = new Guid("55bb048f-9d92-4cbe-a2dd-08dcc73d9c77"),
                            Code = "AF",
                            Name = "Africa",
                            RegionImageUrl = "https://example.com/images/africa.png"
                        },
                        new
                        {
                            Id = new Guid("8a577820-7d28-4bae-a2df-08dcc73d9c77"),
                            Code = "EG",
                            Name = "Egypt",
                            RegionImageUrl = "https://example.com/images/egypt.png"
                        },
                        new
                        {
                            Id = new Guid("36cad70d-0f48-4b63-729e-08dcc742cc2c"),
                            Code = "CN",
                            Name = "China",
                            RegionImageUrl = "https://example.com/images/china.png"
                        },
                        new
                        {
                            Id = new Guid("f79b9c8d-d7e3-4375-9134-24782dbd7f68"),
                            Code = "REG006",
                            Name = "Australia",
                            RegionImageUrl = "http://example.com/region/australia.png"
                        });
                });

            modelBuilder.Entity("NZWalks.API.Models.Domain.Walk", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("DifficultyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("LengthInKm")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RegionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("WalkImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DifficultyId");

                    b.HasIndex("RegionId");

                    b.ToTable("Walks");
                });

            modelBuilder.Entity("NZWalks.API.Models.Domain.Walk", b =>
                {
                    b.HasOne("NZWalks.API.Models.Domain.Difficulty", "Difficulty")
                        .WithMany()
                        .HasForeignKey("DifficultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NZWalks.API.Models.Domain.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Difficulty");

                    b.Navigation("Region");
                });
#pragma warning restore 612, 618
        }
    }
}
