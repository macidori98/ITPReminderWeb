﻿// <auto-generated />
using System;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8");

            modelBuilder.Entity("API.Entities.AppUser", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("BLOB");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("API.Entities.Cars", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AddedBy")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CarType")
                        .HasColumnType("TEXT");

                    b.Property<string>("Comments")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("InspectedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("InspectionInterval")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("InsteptedUntil")
                        .HasColumnType("TEXT");

                    b.Property<string>("OwnerName")
                        .HasColumnType("TEXT");

                    b.Property<string>("OwnerTelNr")
                        .HasColumnType("TEXT");

                    b.Property<string>("PlateNumber")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Cars");
                });
#pragma warning restore 612, 618
        }
    }
}
