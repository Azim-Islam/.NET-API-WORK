﻿// <auto-generated />
using System;
using MagicVilla_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MagicVilla_API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MagicVilla_API.Models.Villa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Occupancy")
                        .HasColumnType("int");

                    b.Property<double>("Rate")
                        .HasColumnType("float");

                    b.Property<int>("Sqft")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Villas");

                    b.HasData(
                        new
                        {
                            Id = 2,
                            CreatedDate = new DateTime(2025, 2, 20, 19, 43, 10, 248, DateTimeKind.Local).AddTicks(4803),
                            Details = "Spacious and modern villa with ocean views",
                            Image = "",
                            Name = "Luxury Retreat",
                            Occupancy = 8,
                            Rate = 350.0,
                            Sqft = 3000,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            CreatedDate = new DateTime(2025, 2, 20, 19, 43, 10, 248, DateTimeKind.Local).AddTicks(5004),
                            Details = "Cozy villa nestled in the mountains",
                            Image = "",
                            Name = "Mountain Escape",
                            Occupancy = 4,
                            Rate = 250.0,
                            Sqft = 1500,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 4,
                            CreatedDate = new DateTime(2025, 2, 20, 19, 43, 10, 248, DateTimeKind.Local).AddTicks(5005),
                            Details = "Villa with direct access to the beach",
                            Image = "",
                            Name = "Beachfront Paradise",
                            Occupancy = 6,
                            Rate = 400.0,
                            Sqft = 2500,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 5,
                            CreatedDate = new DateTime(2025, 2, 20, 19, 43, 10, 248, DateTimeKind.Local).AddTicks(5006),
                            Details = "Modern villa in the heart of the city",
                            Image = "",
                            Name = "Urban Oasis",
                            Occupancy = 3,
                            Rate = 300.0,
                            Sqft = 1200,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 6,
                            CreatedDate = new DateTime(2025, 2, 20, 19, 43, 10, 248, DateTimeKind.Local).AddTicks(5007),
                            Details = "Rustic villa surrounded by nature",
                            Image = "",
                            Name = "Countryside Charm",
                            Occupancy = 7,
                            Rate = 275.0,
                            Sqft = 1800,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("MagicVilla_API.Models.VillaNumber", b =>
                {
                    b.Property<int>("VillaNo")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SpecialDetails")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("VillaID")
                        .HasColumnType("int");

                    b.HasKey("VillaNo");

                    b.HasIndex("VillaID");

                    b.ToTable("VillaNumbers");
                });

            modelBuilder.Entity("MagicVilla_API.Models.VillaNumber", b =>
                {
                    b.HasOne("MagicVilla_API.Models.Villa", "Villa")
                        .WithMany()
                        .HasForeignKey("VillaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Villa");
                });
#pragma warning restore 612, 618
        }
    }
}
