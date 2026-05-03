using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMS.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Department = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CreatedAt", "Department", "Designation", "Email", "FirstName", "JoinDate", "LastName", "Phone", "Salary", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), "Engineering", "Software Engineer", "priya.menon@xyz.com", "Priya", new DateTime(2022, 6, 15, 0, 0, 0, DateTimeKind.Utc), "Menon", "9876543210", 750000m, "Active", new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), "HR", "HR Executive", "rahul.sharma@xyz.com", "Rahul", new DateTime(2021, 3, 10, 0, 0, 0, DateTimeKind.Utc), "Sharma", "9123456780", 500000m, "Inactive", new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), "Marketing", "Marketing Manager", "anita.reddy@xyz.com", "Anita", new DateTime(2020, 8, 20, 0, 0, 0, DateTimeKind.Utc), "Reddy", "9988776655", 680000m, "Active", new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), "Finance", "Accountant", "vikram.patel@xyz.com", "Vikram", new DateTime(2019, 11, 12, 0, 0, 0, DateTimeKind.Utc), "Patel", "9012345678", 620000m, "Active", new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), "Operations", "Operations Manager", "sneha.iyer@xyz.com", "Sneha", new DateTime(2021, 7, 5, 0, 0, 0, DateTimeKind.Utc), "Iyer", "9871234560", 720000m, "Inactive", new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), "Engineering", "Backend Developer", "arjun.nair@xyz.com", "Arjun", new DateTime(2022, 2, 18, 0, 0, 0, DateTimeKind.Utc), "Nair", "9765432109", 800000m, "Active", new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), "HR", "HR Manager", "meera.kapoor@xyz.com", "Meera", new DateTime(2020, 5, 25, 0, 0, 0, DateTimeKind.Utc), "Kapoor", "9654321098", 650000m, "Active", new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                    { 8, new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), "Marketing", "SEO Specialist", "rohit.verma@xyz.com", "Rohit", new DateTime(2021, 9, 14, 0, 0, 0, DateTimeKind.Utc), "Verma", "9543210987", 550000m, "Inactive", new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                    { 9, new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), "Finance", "Financial Analyst", "kavya.shetty@xyz.com", "Kavya", new DateTime(2023, 1, 10, 0, 0, 0, DateTimeKind.Utc), "Shetty", "9432109876", 700000m, "Active", new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                    { 10, new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), "Operations", "Logistics Coordinator", "manoj.singh@xyz.com", "Manoj", new DateTime(2018, 12, 1, 0, 0, 0, DateTimeKind.Utc), "Singh", "9321098765", 480000m, "Inactive", new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                    { 11, new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), "Engineering", "Frontend Developer", "divya.bhat@xyz.com", "Divya", new DateTime(2022, 10, 11, 0, 0, 0, DateTimeKind.Utc), "Bhat", "9210987654", 770000m, "Active", new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                    { 12, new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), "HR", "Recruiter", "suresh.yadav@xyz.com", "Suresh", new DateTime(2021, 4, 22, 0, 0, 0, DateTimeKind.Utc), "Yadav", "9109876543", 450000m, "Active", new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                    { 13, new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), "Marketing", "Content Strategist", "pooja.desai@xyz.com", "Pooja", new DateTime(2020, 2, 17, 0, 0, 0, DateTimeKind.Utc), "Desai", "9098765432", 600000m, "Inactive", new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                    { 14, new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), "Finance", "Auditor", "ajay.kulkarni@xyz.com", "Ajay", new DateTime(2019, 6, 30, 0, 0, 0, DateTimeKind.Utc), "Kulkarni", "9987654321", 730000m, "Active", new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                    { 15, new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), "Operations", "Operations Executive", "neha.gupta@xyz.com", "Neha", new DateTime(2023, 3, 5, 0, 0, 0, DateTimeKind.Utc), "Gupta", "9876501234", 520000m, "Active", new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Email",
                table: "Employees",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Employees");
            migrationBuilder.DropTable(name: "Users");
        }
    }
}
