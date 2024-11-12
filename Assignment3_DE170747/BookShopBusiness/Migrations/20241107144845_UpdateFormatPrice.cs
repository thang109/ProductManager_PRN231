using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShopBusiness.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFormatPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Shippings",
                keyColumn: "ShippingId",
                keyValue: 1,
                columns: new[] { "DateBooking", "DateShip" },
                values: new object[] { new DateTime(2024, 11, 7, 21, 48, 45, 567, DateTimeKind.Local).AddTicks(1289), new DateTime(2024, 11, 9, 21, 48, 45, 567, DateTimeKind.Local).AddTicks(1305) });

            migrationBuilder.UpdateData(
                table: "Shippings",
                keyColumn: "ShippingId",
                keyValue: 2,
                columns: new[] { "DateBooking", "DateShip" },
                values: new object[] { new DateTime(2024, 11, 7, 21, 48, 45, 567, DateTimeKind.Local).AddTicks(1318), new DateTime(2024, 11, 10, 21, 48, 45, 567, DateTimeKind.Local).AddTicks(1319) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Shippings",
                keyColumn: "ShippingId",
                keyValue: 1,
                columns: new[] { "DateBooking", "DateShip" },
                values: new object[] { new DateTime(2024, 11, 7, 19, 29, 35, 402, DateTimeKind.Local).AddTicks(6748), new DateTime(2024, 11, 9, 19, 29, 35, 402, DateTimeKind.Local).AddTicks(6789) });

            migrationBuilder.UpdateData(
                table: "Shippings",
                keyColumn: "ShippingId",
                keyValue: 2,
                columns: new[] { "DateBooking", "DateShip" },
                values: new object[] { new DateTime(2024, 11, 7, 19, 29, 35, 402, DateTimeKind.Local).AddTicks(6814), new DateTime(2024, 11, 10, 19, 29, 35, 402, DateTimeKind.Local).AddTicks(6815) });
        }
    }
}
