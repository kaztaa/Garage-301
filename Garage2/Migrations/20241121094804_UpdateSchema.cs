using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Garage301.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "VehicleTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Car" },
                    { 2, "Motorcycle" },
                    { 3, "Truck" },
                    { 4, "Bus" },
                    { 5, "Van" },
                    { 6, "Bicycle" }
                });

            migrationBuilder.InsertData(
                table: "ParkedVehicle",
                columns: new[] { "Id", "ArrivalTime", "CheckoutTime", "Color", "Make", "Model", "NumberOfWheels", "ParkingSpotId", "RegistrationNumber", "VehicleTypesId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 21, 8, 38, 5, 467, DateTimeKind.Local).AddTicks(1319), null, "Blue", "Toyota", "Corolla", 4, null, "ABC123", 1 },
                    { 2, new DateTime(2024, 11, 21, 8, 38, 5, 467, DateTimeKind.Local).AddTicks(1569), null, "Green", "Hyundai", "i3", 4, null, "ERT234", 1 },
                    { 3, new DateTime(2024, 11, 21, 8, 38, 5, 467, DateTimeKind.Local).AddTicks(1573), null, "Black", "BMW", "M3", 4, null, "ERR134", 1 },
                    { 4, new DateTime(2024, 11, 21, 8, 38, 5, 467, DateTimeKind.Local).AddTicks(1576), null, "Red", "Honda", "Goldwing", 2, null, "HFF577", 2 },
                    { 5, new DateTime(2024, 11, 21, 8, 38, 5, 467, DateTimeKind.Local).AddTicks(1580), null, "Green", "Yamaha", "R1", 2, null, "OOP123", 2 }
                });
        }
    }
}
