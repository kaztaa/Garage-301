using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Garage301.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VehicleTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParkedVehicle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleTypesId = table.Column<int>(type: "int", nullable: false),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Make = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    NumberOfWheels = table.Column<int>(type: "int", nullable: false),
                    ParkingSpotId = table.Column<int>(type: "int", nullable: true),
                    ArrivalTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckoutTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkedVehicle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParkedVehicle_VehicleTypes_VehicleTypesId",
                        column: x => x.VehicleTypesId,
                        principalTable: "VehicleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ParkingSpot",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpotNumber = table.Column<int>(type: "int", nullable: false),
                    IsOccupied = table.Column<bool>(type: "bit", nullable: false),
                    ParkedVehicleId = table.Column<int>(type: "int", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingSpot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParkingSpot_ParkedVehicle_ParkedVehicleId",
                        column: x => x.ParkedVehicleId,
                        principalTable: "ParkedVehicle",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "ParkingSpot",
                columns: new[] { "Id", "IsOccupied", "Location", "ParkedVehicleId", "SpotNumber" },
                values: new object[,]
                {
                    { 1, false, null, null, 1 },
                    { 2, false, null, null, 2 },
                    { 3, false, null, null, 3 },
                    { 4, false, null, null, 4 },
                    { 5, false, null, null, 5 },
                    { 6, false, null, null, 6 },
                    { 7, false, null, null, 7 },
                    { 8, false, null, null, 8 },
                    { 9, false, null, null, 9 },
                    { 10, false, null, null, 10 },
                    { 11, false, null, null, 11 },
                    { 12, false, null, null, 12 }
                });

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
                    { 1, new DateTime(2024, 11, 21, 8, 1, 13, 46, DateTimeKind.Local).AddTicks(1782), null, "Blue", "Toyota", "Corolla", 4, null, "ABC123", 1 },
                    { 2, new DateTime(2024, 11, 21, 8, 1, 13, 46, DateTimeKind.Local).AddTicks(1787), null, "Green", "Hyundai", "i3", 4, null, "ERT234", 1 },
                    { 3, new DateTime(2024, 11, 21, 8, 1, 13, 46, DateTimeKind.Local).AddTicks(1791), null, "Black", "BMW", "M3", 4, null, "ERR134", 1 },
                    { 4, new DateTime(2024, 11, 21, 8, 1, 13, 46, DateTimeKind.Local).AddTicks(1795), null, "Red", "Honda", "Goldwing", 2, null, "HFF577", 2 },
                    { 5, new DateTime(2024, 11, 21, 8, 1, 13, 46, DateTimeKind.Local).AddTicks(1799), null, "Green", "Yamaha", "R1", 2, null, "OOP123", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParkedVehicle_VehicleTypesId",
                table: "ParkedVehicle",
                column: "VehicleTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSpot_ParkedVehicleId",
                table: "ParkingSpot",
                column: "ParkedVehicleId",
                unique: true,
                filter: "[ParkedVehicleId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParkingSpot");

            migrationBuilder.DropTable(
                name: "ParkedVehicle");

            migrationBuilder.DropTable(
                name: "VehicleTypes");
        }
    }
}
