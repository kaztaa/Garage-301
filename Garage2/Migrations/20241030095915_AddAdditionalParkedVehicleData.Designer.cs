﻿// <auto-generated />
using System;
using Garage2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Garage2.Migrations
{
    [DbContext(typeof(Garage2Context))]
    [Migration("20241030095915_AddAdditionalParkedVehicleData")]
    partial class AddAdditionalParkedVehicleData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Garage2.Models.ParkedVehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ArrivalTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Make")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfWheels")
                        .HasColumnType("int");

                    b.Property<string>("RegistrationNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VehicleType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ParkedVehicle");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ArrivalTime = new DateTime(2024, 10, 30, 8, 59, 15, 368, DateTimeKind.Local).AddTicks(5828),
                            Color = "Blue",
                            Make = "Toyota",
                            Model = "Corolla",
                            NumberOfWheels = 4,
                            RegistrationNumber = "ABC123",
                            VehicleType = "Car"
                        },
                        new
                        {
                            Id = 2,
                            ArrivalTime = new DateTime(2024, 10, 30, 8, 59, 15, 368, DateTimeKind.Local).AddTicks(5880),
                            Color = "Green",
                            Make = "Hundai",
                            Model = "i3",
                            NumberOfWheels = 4,
                            RegistrationNumber = "ERT234",
                            VehicleType = "Car"
                        },
                        new
                        {
                            Id = 3,
                            ArrivalTime = new DateTime(2024, 10, 30, 8, 59, 15, 368, DateTimeKind.Local).AddTicks(5882),
                            Color = "Balck",
                            Make = "BMW",
                            Model = "M3",
                            NumberOfWheels = 4,
                            RegistrationNumber = "ERR134",
                            VehicleType = "Car"
                        },
                        new
                        {
                            Id = 4,
                            ArrivalTime = new DateTime(2024, 10, 30, 8, 59, 15, 368, DateTimeKind.Local).AddTicks(5885),
                            Color = "Red",
                            Make = "Honda",
                            Model = "Goldwing",
                            NumberOfWheels = 4,
                            RegistrationNumber = "HFF577",
                            VehicleType = "Motorcycle"
                        },
                        new
                        {
                            Id = 5,
                            ArrivalTime = new DateTime(2024, 10, 30, 8, 59, 15, 368, DateTimeKind.Local).AddTicks(5888),
                            Color = "Green",
                            Make = "Yamaha",
                            Model = "R1",
                            NumberOfWheels = 4,
                            RegistrationNumber = "OOP123",
                            VehicleType = "Motorcycle"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
