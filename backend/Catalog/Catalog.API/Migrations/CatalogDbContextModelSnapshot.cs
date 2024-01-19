﻿// <auto-generated />
using System;
using Catalog.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Catalog.API.Migrations
{
    [DbContext(typeof(CatalogDbContext))]
    partial class CatalogDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Catalog.DataAccess.Entities.BrandEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Brand", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTimeOffset(new DateTime(2024, 1, 19, 21, 55, 37, 440, DateTimeKind.Unspecified).AddTicks(2983), new TimeSpan(0, 2, 0, 0, 0)),
                            Title = "Nike"
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTimeOffset(new DateTime(2024, 1, 19, 21, 55, 37, 440, DateTimeKind.Unspecified).AddTicks(2987), new TimeSpan(0, 2, 0, 0, 0)),
                            Title = "Adidas"
                        });
                });

            modelBuilder.Entity("Catalog.DataAccess.Entities.ItemEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BrandId")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<string>("PictureFile")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("TypeId")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("TypeId");

                    b.ToTable("Item", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BrandId = 1,
                            CreatedAt = new DateTimeOffset(new DateTime(2024, 1, 19, 21, 55, 37, 440, DateTimeKind.Unspecified).AddTicks(2995), new TimeSpan(0, 2, 0, 0, 0)),
                            Description = "Created for the hardwood but taken to the streets, the '80s b-ball icon returns with classic details and throwback hoops flair. ",
                            PictureFile = "nike-dunk-low-retro-premium.png",
                            Price = 100m,
                            Title = "Nike Dunk Low Retro Premium",
                            TypeId = 1
                        },
                        new
                        {
                            Id = 2,
                            BrandId = 1,
                            CreatedAt = new DateTimeOffset(new DateTime(2024, 1, 19, 21, 55, 37, 440, DateTimeKind.Unspecified).AddTicks(2999), new TimeSpan(0, 2, 0, 0, 0)),
                            Description = "Created for the hardwood but taken to the streets, the '80s b-ball icon returns with crisp leather and and classic \"Panda\" color blocking.",
                            PictureFile = "nike-dunk-mid.png",
                            Price = 120m,
                            Title = "Nike Dunk Mid",
                            TypeId = 1
                        },
                        new
                        {
                            Id = 3,
                            BrandId = 1,
                            CreatedAt = new DateTimeOffset(new DateTime(2024, 1, 19, 21, 55, 37, 440, DateTimeKind.Unspecified).AddTicks(3001), new TimeSpan(0, 2, 0, 0, 0)),
                            Description = "Meet your new go-to hoodie. Heavyweight fleece feels super soft, and the comfy, relaxed fit will have you reaching for it again and again.",
                            PictureFile = "jordan-flight-fleece.png",
                            Price = 100m,
                            Title = "Jordan Flight Fleece",
                            TypeId = 2
                        },
                        new
                        {
                            Id = 4,
                            BrandId = 2,
                            CreatedAt = new DateTimeOffset(new DateTime(2024, 1, 19, 21, 55, 37, 440, DateTimeKind.Unspecified).AddTicks(3005), new TimeSpan(0, 2, 0, 0, 0)),
                            Description = "These are not just sneakers, but a real symbol of the era. The adidas Forum model appeared in 1984 and won love not only on basketball courts, but also in show business.",
                            PictureFile = "forum-low.png",
                            Price = 110m,
                            Title = "Forum Low",
                            TypeId = 1
                        },
                        new
                        {
                            Id = 5,
                            BrandId = 2,
                            CreatedAt = new DateTimeOffset(new DateTime(2024, 1, 19, 21, 55, 37, 440, DateTimeKind.Unspecified).AddTicks(3007), new TimeSpan(0, 2, 0, 0, 0)),
                            Description = "For your team. For your planet. Created with grassroots football in mind, the Entrada 22 range gives you everything you need to make your game feel and look more beautiful. ",
                            PictureFile = "entrada-22-sweat-hoodie.png",
                            Price = 60m,
                            Title = "ENTRADA 22 SWEAT HOODIE",
                            TypeId = 2
                        },
                        new
                        {
                            Id = 6,
                            BrandId = 1,
                            CreatedAt = new DateTimeOffset(new DateTime(2024, 1, 19, 21, 55, 37, 440, DateTimeKind.Unspecified).AddTicks(3011), new TimeSpan(0, 2, 0, 0, 0)),
                            Description = "Ja Morant became the superstar he is today by repeatedly sinking jumpers on crooked rims, jumping on tractor tires and dribbling through traffic cones in steamy South Carolina summers.",
                            PictureFile = "ja-1.png",
                            Price = 130m,
                            Title = "Ja 1",
                            TypeId = 1
                        },
                        new
                        {
                            Id = 7,
                            BrandId = 1,
                            CreatedAt = new DateTimeOffset(new DateTime(2024, 1, 19, 21, 55, 37, 440, DateTimeKind.Unspecified).AddTicks(3014), new TimeSpan(0, 2, 0, 0, 0)),
                            Description = "Feel unbeatable, from the tee box to the final putt. Inspired by one of the most iconic sneakers of all time, the Air Jordan 1 G is an instant classic on the course. ",
                            PictureFile = "air-jordan-i-high.png",
                            Price = 190m,
                            Title = "Air Jordan I High G",
                            TypeId = 1
                        });
                });

            modelBuilder.Entity("Catalog.DataAccess.Entities.TypeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Type", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTimeOffset(new DateTime(2024, 1, 19, 21, 55, 37, 440, DateTimeKind.Unspecified).AddTicks(2927), new TimeSpan(0, 2, 0, 0, 0)),
                            Title = "Shoes"
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTimeOffset(new DateTime(2024, 1, 19, 21, 55, 37, 440, DateTimeKind.Unspecified).AddTicks(2978), new TimeSpan(0, 2, 0, 0, 0)),
                            Title = "Hoodie"
                        });
                });

            modelBuilder.Entity("Catalog.DataAccess.Entities.ItemEntity", b =>
                {
                    b.HasOne("Catalog.DataAccess.Entities.BrandEntity", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Catalog.DataAccess.Entities.TypeEntity", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Type");
                });
#pragma warning restore 612, 618
        }
    }
}
