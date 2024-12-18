﻿// <auto-generated />
using System;
using Garage301.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Garage301.Migrations
{
    [DbContext(typeof(Garage301Context))]
    partial class Garage301ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Garage301.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Personnummer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "user1",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "4ccfd3c7-71c6-4dd9-8bd2-08467dc5648b",
                            Email = "user1@example.com",
                            EmailConfirmed = false,
                            FirstName = "user1",
                            LastName = "Usersson1",
                            LockoutEnabled = false,
                            Personnummer = "",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "cc95ae38-361a-46a3-83dd-f17a43184ad4",
                            TwoFactorEnabled = false,
                            UserName = "UserName1"
                        },
                        new
                        {
                            Id = "user2",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "0d9cc229-e5f0-438b-b293-5a77e8ecaa86",
                            Email = "user2@example.com",
                            EmailConfirmed = false,
                            FirstName = "user2",
                            LastName = "Usersson2",
                            LockoutEnabled = false,
                            Personnummer = "",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "0d243819-0319-42c1-97c1-477d627f5ef0",
                            TwoFactorEnabled = false,
                            UserName = "UserName2"
                        });
                });

            modelBuilder.Entity("Garage301.Models.ParkedVehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ApplicationUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

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

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("VehicleTypesId");

                    b.ToTable("ParkedVehicle");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ApplicationUserId = "user1",
                            ArrivalTime = new DateTime(2024, 11, 25, 6, 46, 15, 997, DateTimeKind.Local).AddTicks(3132),
                            Color = "#0000FF",
                            Make = "Toyota",
                            Model = "Corolla",
                            NumberOfWheels = 4,
                            ParkingSpotId = 1,
                            RegistrationNumber = "ABC123",
                            VehicleTypesId = 1
                        },
                        new
                        {
                            Id = 2,
                            ApplicationUserId = "user2",
                            ArrivalTime = new DateTime(2024, 11, 25, 6, 46, 15, 997, DateTimeKind.Local).AddTicks(3138),
                            Color = "#008000",
                            Make = "Hyundai",
                            Model = "i3",
                            NumberOfWheels = 4,
                            ParkingSpotId = 2,
                            RegistrationNumber = "ERT234",
                            VehicleTypesId = 1
                        },
                        new
                        {
                            Id = 3,
                            ApplicationUserId = "user2",
                            ArrivalTime = new DateTime(2024, 11, 25, 6, 46, 15, 997, DateTimeKind.Local).AddTicks(3142),
                            Color = "#000000",
                            Make = "BMW",
                            Model = "M3",
                            NumberOfWheels = 4,
                            ParkingSpotId = 3,
                            RegistrationNumber = "ERR134",
                            VehicleTypesId = 1
                        },
                        new
                        {
                            Id = 4,
                            ApplicationUserId = "user1",
                            ArrivalTime = new DateTime(2024, 11, 25, 6, 46, 15, 997, DateTimeKind.Local).AddTicks(3146),
                            Color = "#FF0000",
                            Make = "Honda",
                            Model = "Goldwing",
                            NumberOfWheels = 2,
                            ParkingSpotId = 4,
                            RegistrationNumber = "HFF577",
                            VehicleTypesId = 2
                        },
                        new
                        {
                            Id = 5,
                            ApplicationUserId = "user2",
                            ArrivalTime = new DateTime(2024, 11, 25, 6, 46, 15, 997, DateTimeKind.Local).AddTicks(3150),
                            Color = "#008000",
                            Make = "Yamaha",
                            Model = "R1",
                            NumberOfWheels = 2,
                            ParkingSpotId = 5,
                            RegistrationNumber = "OOP123",
                            VehicleTypesId = 2
                        });
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
                            IsOccupied = true,
                            Location = "Location 1",
                            ParkedVehicleId = 1,
                            SpotNumber = 1
                        },
                        new
                        {
                            Id = 2,
                            IsOccupied = true,
                            Location = "Location 2",
                            ParkedVehicleId = 2,
                            SpotNumber = 2
                        },
                        new
                        {
                            Id = 3,
                            IsOccupied = true,
                            Location = "Location 3",
                            ParkedVehicleId = 3,
                            SpotNumber = 3
                        },
                        new
                        {
                            Id = 4,
                            IsOccupied = true,
                            Location = "Location 4",
                            ParkedVehicleId = 4,
                            SpotNumber = 4
                        },
                        new
                        {
                            Id = 5,
                            IsOccupied = true,
                            Location = "Location 5",
                            ParkedVehicleId = 5,
                            SpotNumber = 5
                        },
                        new
                        {
                            Id = 6,
                            IsOccupied = false,
                            Location = "Location 6",
                            SpotNumber = 6
                        },
                        new
                        {
                            Id = 7,
                            IsOccupied = false,
                            Location = "Location 7",
                            SpotNumber = 7
                        },
                        new
                        {
                            Id = 8,
                            IsOccupied = false,
                            Location = "Location 8",
                            SpotNumber = 8
                        },
                        new
                        {
                            Id = 9,
                            IsOccupied = false,
                            Location = "Location 9",
                            SpotNumber = 9
                        },
                        new
                        {
                            Id = 10,
                            IsOccupied = false,
                            Location = "Location 10",
                            SpotNumber = 10
                        },
                        new
                        {
                            Id = 11,
                            IsOccupied = false,
                            Location = "Location 11",
                            SpotNumber = 11
                        },
                        new
                        {
                            Id = 12,
                            IsOccupied = false,
                            Location = "Location 12",
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

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Car"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Motorcycle"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Truck"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Bus"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Van"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Bicycle"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Garage301.Models.ParkedVehicle", b =>
                {
                    b.HasOne("Garage301.Models.ApplicationUser", "User")
                        .WithMany("Vehicles")
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Garage301.Models.VehicleTypes", "VehicleType")
                        .WithMany("ParkedVehicles")
                        .HasForeignKey("VehicleTypesId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("VehicleType");
                });

            modelBuilder.Entity("Garage301.Models.ParkingSpot", b =>
                {
                    b.HasOne("Garage301.Models.ParkedVehicle", "ParkedVehicle")
                        .WithOne("ParkingSpot")
                        .HasForeignKey("Garage301.Models.ParkingSpot", "ParkedVehicleId");

                    b.Navigation("ParkedVehicle");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Garage301.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Garage301.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Garage301.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Garage301.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Garage301.Models.ApplicationUser", b =>
                {
                    b.Navigation("Vehicles");
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
