using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthenLearn.Migrations
{
    /// <inheritdoc />
    public partial class UpdateExpenseSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: new Guid("bc580c44-2332-4457-99c9-5ea4c096690a"),
                column: "Date",
                value: new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: new Guid("bc580c44-2332-4457-99c9-5ea4c096690a"),
                column: "Date",
                value: new DateTime(2025, 7, 24, 11, 8, 48, 470, DateTimeKind.Local).AddTicks(2076));
        }
    }
}
