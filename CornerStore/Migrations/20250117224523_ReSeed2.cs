using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CornerStore.Migrations
{
    /// <inheritdoc />
    public partial class ReSeed2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaidOnDate",
                value: new DateTime(2025, 1, 17, 16, 45, 23, 430, DateTimeKind.Local).AddTicks(7446));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "PaidOnDate",
                value: new DateTime(2025, 1, 17, 16, 45, 23, 430, DateTimeKind.Local).AddTicks(7550));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaidOnDate",
                value: new DateTime(2025, 1, 17, 16, 45, 12, 253, DateTimeKind.Local).AddTicks(7910));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "PaidOnDate",
                value: new DateTime(2025, 1, 17, 16, 45, 12, 253, DateTimeKind.Local).AddTicks(7964));
        }
    }
}
