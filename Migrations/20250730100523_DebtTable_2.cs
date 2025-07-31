using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthenLearn.Migrations
{
    /// <inheritdoc />
    public partial class DebtTable_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Expenses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Debts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: new Guid("bc580c44-2332-4457-99c9-5ea4c096690a"),
                column: "Type",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Debts");
        }
    }
}
