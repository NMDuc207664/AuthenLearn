using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthenLearn.Migrations
{
    /// <inheritdoc />
    public partial class new_Expense_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_user_account_UserId",
                        column: x => x.UserId,
                        principalTable: "user_account",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Amount", "Date", "Description", "UserId" },
                values: new object[] { new Guid("bc580c44-2332-4457-99c9-5ea4c096690a"), 1000000.0, new DateTime(2025, 7, 24, 11, 8, 48, 470, DateTimeKind.Local).AddTicks(2076), "tiền lương tháng 7", new Guid("4090476f-2e7e-4884-9eca-08ddc9cc4038") });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_UserId",
                table: "Expenses",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expenses");
        }
    }
}
