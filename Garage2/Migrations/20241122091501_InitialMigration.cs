using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Garage301.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user2");

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 1,
                column: "ArrivalTime",
                value: new DateTime(2024, 11, 22, 8, 14, 59, 337, DateTimeKind.Local).AddTicks(9208));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 2,
                column: "ArrivalTime",
                value: new DateTime(2024, 11, 22, 8, 14, 59, 337, DateTimeKind.Local).AddTicks(9354));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 3,
                column: "ArrivalTime",
                value: new DateTime(2024, 11, 22, 8, 14, 59, 337, DateTimeKind.Local).AddTicks(9360));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 4,
                column: "ArrivalTime",
                value: new DateTime(2024, 11, 22, 8, 14, 59, 337, DateTimeKind.Local).AddTicks(9365));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 5,
                column: "ArrivalTime",
                value: new DateTime(2024, 11, 22, 8, 14, 59, 337, DateTimeKind.Local).AddTicks(9370));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Personnummer", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "user1", 0, "604eb3a5-3f4b-4329-aaf4-9135da3b21b2", "user1@example.com", false, "user1", "Ehds", false, null, null, null, null, "", null, false, "c33f0c02-1a2e-44d2-83b5-42fa7af45f8e", false, null },
                    { "user2", 0, "cd98163e-00fa-4345-9932-537dc0a4e6fa", "user2@example.com", false, "user2", "Ehdsafd", false, null, null, null, null, "", null, false, "19ed55a7-1ea3-4f89-a441-bb27d923bc0a", false, null }
                });

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 1,
                column: "ArrivalTime",
                value: new DateTime(2024, 11, 22, 8, 0, 7, 184, DateTimeKind.Local).AddTicks(5632));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 2,
                column: "ArrivalTime",
                value: new DateTime(2024, 11, 22, 8, 0, 7, 184, DateTimeKind.Local).AddTicks(5639));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 3,
                column: "ArrivalTime",
                value: new DateTime(2024, 11, 22, 8, 0, 7, 184, DateTimeKind.Local).AddTicks(5643));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 4,
                column: "ArrivalTime",
                value: new DateTime(2024, 11, 22, 8, 0, 7, 184, DateTimeKind.Local).AddTicks(5647));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 5,
                column: "ArrivalTime",
                value: new DateTime(2024, 11, 22, 8, 0, 7, 184, DateTimeKind.Local).AddTicks(5651));
        }
    }
}
