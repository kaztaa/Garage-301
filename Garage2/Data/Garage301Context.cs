using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Humanizer.Localisation;
using Garage301.Models;

namespace Garage301.Data
{
    public class Garage301Context : DbContext
    {
        public Garage301Context(DbContextOptions<Garage301Context> options)
            : base(options)
        {
        }

        public DbSet<ParkedVehicle> ParkedVehicle { get; set; } = default!;
        public DbSet<ParkingSpot> ParkingSpot { get; set; } = default!;
        public DbSet<VehicleTypes> VehicleTypes { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the relationship between ParkedVehicle and VehicleTypes
            modelBuilder.Entity<ParkedVehicle>()
                .HasOne(pv => pv.VehicleType)
                .WithMany(vt => vt.ParkedVehicles)
                .HasForeignKey(pv => pv.VehicleTypesId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure one-to-one relationship between ParkedVehicle and ParkingSpot
            modelBuilder.Entity<ParkedVehicle>()
                .HasOne(pv => pv.ParkingSpot)
                .WithOne(ps => ps.ParkedVehicle)
                .HasForeignKey<ParkingSpot>(ps => ps.ParkedVehicleId);

            // Seed data for VehicleTypes
            modelBuilder.Entity<VehicleTypes>().HasData(
                new VehicleTypes { Id = 1, Name = "Car" },
                new VehicleTypes { Id = 2, Name = "Motorcycle" },
                new VehicleTypes { Id = 3, Name = "Truck" },
                new VehicleTypes { Id = 4, Name = "Bus" },
                new VehicleTypes { Id = 5, Name = "Van" },
                new VehicleTypes { Id = 6, Name = "Bicycle" }
            );

            //// Seed data for ParkedVehicle with only foreign keys specified
            //modelBuilder.Entity<ParkedVehicle>().HasData(
            //    new ParkedVehicle { Id = 1, VehicleTypesId = 1, RegistrationNumber = "ABC123", Color = "Blue", Make = "Toyota", Model = "Corolla", NumberOfWheels = 4, ArrivalTime = DateTime.Now.AddHours(-2) },
            //    new ParkedVehicle { Id = 2, VehicleTypesId = 1, RegistrationNumber = "ERT234", Color = "Green", Make = "Hyundai", Model = "i3", NumberOfWheels = 4, ArrivalTime = DateTime.Now.AddHours(-2) },
            //    new ParkedVehicle { Id = 3, VehicleTypesId = 1, RegistrationNumber = "ERR134", Color = "Black", Make = "BMW", Model = "M3", NumberOfWheels = 4, ArrivalTime = DateTime.Now.AddHours(-2) },
            //    new ParkedVehicle { Id = 4, VehicleTypesId = 2, RegistrationNumber = "HFF577", Color = "Red", Make = "Honda", Model = "Goldwing", NumberOfWheels = 2, ArrivalTime = DateTime.Now.AddHours(-2) },
            //    new ParkedVehicle { Id = 5, VehicleTypesId = 2, RegistrationNumber = "OOP123", Color = "Green", Make = "Yamaha", Model = "R1", NumberOfWheels = 2, ArrivalTime = DateTime.Now.AddHours(-2) }
            //);

            // Seed data for ParkingSpot
            for (int i = 1; i <= 12; i++)
            {
                modelBuilder.Entity<ParkingSpot>().HasData(new ParkingSpot
                {
                    Id = i,
                    SpotNumber = i,
                    IsOccupied = false
                });
            }
        }
    }
}
