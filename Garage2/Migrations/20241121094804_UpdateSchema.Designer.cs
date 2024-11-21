﻿// <auto-generated />
using System;
using Garage301.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Garage301.Migrations
{
    [DbContext(typeof(Garage301Context))]
    [Migration("20241121094804_UpdateSchema")]
    partial class UpdateSchema
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Garage301.Models.ParkedVehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ArrivalTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CheckoutTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Make")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<int>("NumberOfWheels")
                        .HasColumnType("int");

                    b.Property<int?>("ParkingSpotId")
                        .HasColumnType("int");

                    b.Property<string>("RegistrationNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VehicleTypesId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VehicleTypesId");

                    b.ToTable("ParkedVehicle");
                });

            modelBuilder.Entity("Garage301.Models.ParkingSpot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsOccupied")
                        .HasColumnType("bit");

                    b.Property<string>("Location")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("ParkedVehicleId")
                        .HasColumnType("int");

                    b.Property<int>("SpotNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParkedVehicleId")
                        .IsUnique()
                        .HasFilter("[ParkedVehicleId] IS NOT NULL");

                    b.ToTable("ParkingSpot");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsOccupied = false,
                            SpotNumber = 1
                        },
                        new
                        {
                            Id = 2,
                            IsOccupied = false,
                            SpotNumber = 2
                        },
                        new
                        {
                            Id = 3,
                            IsOccupied = false,
                            SpotNumber = 3
                        },
                        new
                        {
                            Id = 4,
                            IsOccupied = false,
                            SpotNumber = 4
                        },
                        new
                        {
                            Id = 5,
                            IsOccupied = false,
                            SpotNumber = 5
                        },
                        new
                        {
                            Id = 6,
                            IsOccupied = false,
                            SpotNumber = 6
                        },
                        new
                        {
                            Id = 7,
                            IsOccupied = false,
                            SpotNumber = 7
                        },
                        new
                        {
                            Id = 8,
                            IsOccupied = false,
                            SpotNumber = 8
                        },
                        new
                        {
                            Id = 9,
                            IsOccupied = false,
                            SpotNumber = 9
                        },
                        new
                        {
                            Id = 10,
                            IsOccupied = false,
                            SpotNumber = 10
                        },
                        new
                        {
                            Id = 11,
                            IsOccupied = false,
                            SpotNumber = 11
                        },
                        new
                        {
                            Id = 12,
                            IsOccupied = false,
                            SpotNumber = 12
                        });
                });

            modelBuilder.Entity("Garage301.Models.VehicleTypes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("VehicleTypes");
                });

            modelBuilder.Entity("Garage301.Models.ParkedVehicle", b =>
                {
                    b.HasOne("Garage301.Models.VehicleTypes", "VehicleType")
                        .WithMany("ParkedVehicles")
                        .HasForeignKey("VehicleTypesId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("VehicleType");
                });

            modelBuilder.Entity("Garage301.Models.ParkingSpot", b =>
                {
                    b.HasOne("Garage301.Models.ParkedVehicle", "ParkedVehicle")
                        .WithOne("ParkingSpot")
                        .HasForeignKey("Garage301.Models.ParkingSpot", "ParkedVehicleId");

                    b.Navigation("ParkedVehicle");
                });

            modelBuilder.Entity("Garage301.Models.ParkedVehicle", b =>
                {
                    b.Navigation("ParkingSpot");
                });

            modelBuilder.Entity("Garage301.Models.VehicleTypes", b =>
                {
                    b.Navigation("ParkedVehicles");
                });
#pragma warning restore 612, 618
        }
    }
}
