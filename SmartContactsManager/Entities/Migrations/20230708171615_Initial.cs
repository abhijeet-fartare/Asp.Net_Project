using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactTable",
                columns: table => new
                {
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactTable", x => x.ContactId);
                });

            migrationBuilder.InsertData(
                table: "ContactTable",
                columns: new[] { "ContactId", "Description", "Email", "Gender", "Name", "Phone" },
                values: new object[,]
                {
                    { new Guid("1daa30c5-1a0b-40de-b0ed-f703010252ce"), "Actor", "abhijeet@gmail.com", "MALE", "Abhijeet", "8557225817" },
                    { new Guid("77175a53-aa4f-404b-ae0f-28d95b2ef0c7"), "Student", "sonu@gmail.com", "FEMALE", "Sonu", "7057222817" },
                    { new Guid("7d1afe41-3007-4832-bb94-6ec9d06dc0d4"), "Actor", "ketan@gmail.com", "MALE", "Ketan", "9057225817" },
                    { new Guid("e3d2d0a6-7c4a-404b-822c-a2a910c52265"), "Teacher", "raj@gmail.com", "MALE", "Raj", "8557222817" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactTable");
        }
    }
}
