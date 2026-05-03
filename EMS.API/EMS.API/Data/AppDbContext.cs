using EMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EMS.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<AppUser> Users => Set<AppUser>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Employee>()
            .HasIndex(e => e.Email)
            .IsUnique();

        modelBuilder.Entity<Employee>()
            .Property(e => e.Salary)
            .HasPrecision(18, 2);

        modelBuilder.Entity<AppUser>()
            .HasIndex(u => u.Username)
            .IsUnique();

        var baseDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        modelBuilder.Entity<Employee>().HasData(
            new Employee
            {
                Id = 1,
                FirstName = "Priya",
                LastName = "Menon",
                Email = "priya.menon@xyz.com",
                Phone = "9876543210",
                Department = "Engineering",
                Designation = "Software Engineer",
                Salary = 750000m,
                JoinDate = new DateTime(2022, 6, 15, 0, 0, 0, DateTimeKind.Utc),
                Status = "Active",
                CreatedAt = baseDate,
                UpdatedAt = baseDate
            },
            new Employee
            {
                Id = 2,
                FirstName = "Rahul",
                LastName = "Sharma",
                Email = "rahul.sharma@xyz.com",
                Phone = "9123456780",
                Department = "HR",
                Designation = "HR Executive",
                Salary = 500000m,
                JoinDate = new DateTime(2021, 3, 10, 0, 0, 0, DateTimeKind.Utc),
                Status = "Inactive",
                CreatedAt = baseDate,
                UpdatedAt = baseDate
            },
            new Employee
            {
                Id = 3,
                FirstName = "Anita",
                LastName = "Reddy",
                Email = "anita.reddy@xyz.com",
                Phone = "9988776655",
                Department = "Marketing",
                Designation = "Marketing Manager",
                Salary = 680000m,
                JoinDate = new DateTime(2020, 8, 20, 0, 0, 0, DateTimeKind.Utc),
                Status = "Active",
                CreatedAt = baseDate,
                UpdatedAt = baseDate
            },
            new Employee
            {
                Id = 4,
                FirstName = "Vikram",
                LastName = "Patel",
                Email = "vikram.patel@xyz.com",
                Phone = "9012345678",
                Department = "Finance",
                Designation = "Accountant",
                Salary = 620000m,
                JoinDate = new DateTime(2019, 11, 12, 0, 0, 0, DateTimeKind.Utc),
                Status = "Active",
                CreatedAt = baseDate,
                UpdatedAt = baseDate
            },
            new Employee
            {
                Id = 5,
                FirstName = "Sneha",
                LastName = "Iyer",
                Email = "sneha.iyer@xyz.com",
                Phone = "9871234560",
                Department = "Operations",
                Designation = "Operations Manager",
                Salary = 720000m,
                JoinDate = new DateTime(2021, 7, 5, 0, 0, 0, DateTimeKind.Utc),
                Status = "Inactive",
                CreatedAt = baseDate,
                UpdatedAt = baseDate
            },
            new Employee
            {
                Id = 6,
                FirstName = "Arjun",
                LastName = "Nair",
                Email = "arjun.nair@xyz.com",
                Phone = "9765432109",
                Department = "Engineering",
                Designation = "Backend Developer",
                Salary = 800000m,
                JoinDate = new DateTime(2022, 2, 18, 0, 0, 0, DateTimeKind.Utc),
                Status = "Active",
                CreatedAt = baseDate,
                UpdatedAt = baseDate
            },
            new Employee
            {
                Id = 7,
                FirstName = "Meera",
                LastName = "Kapoor",
                Email = "meera.kapoor@xyz.com",
                Phone = "9654321098",
                Department = "HR",
                Designation = "HR Manager",
                Salary = 650000m,
                JoinDate = new DateTime(2020, 5, 25, 0, 0, 0, DateTimeKind.Utc),
                Status = "Active",
                CreatedAt = baseDate,
                UpdatedAt = baseDate
            },
            new Employee
            {
                Id = 8,
                FirstName = "Rohit",
                LastName = "Verma",
                Email = "rohit.verma@xyz.com",
                Phone = "9543210987",
                Department = "Marketing",
                Designation = "SEO Specialist",
                Salary = 550000m,
                JoinDate = new DateTime(2021, 9, 14, 0, 0, 0, DateTimeKind.Utc),
                Status = "Inactive",
                CreatedAt = baseDate,
                UpdatedAt = baseDate
            },
            new Employee
            {
                Id = 9,
                FirstName = "Kavya",
                LastName = "Shetty",
                Email = "kavya.shetty@xyz.com",
                Phone = "9432109876",
                Department = "Finance",
                Designation = "Financial Analyst",
                Salary = 700000m,
                JoinDate = new DateTime(2023, 1, 10, 0, 0, 0, DateTimeKind.Utc),
                Status = "Active",
                CreatedAt = baseDate,
                UpdatedAt = baseDate
            },
            new Employee
            {
                Id = 10,
                FirstName = "Manoj",
                LastName = "Singh",
                Email = "manoj.singh@xyz.com",
                Phone = "9321098765",
                Department = "Operations",
                Designation = "Logistics Coordinator",
                Salary = 480000m,
                JoinDate = new DateTime(2018, 12, 1, 0, 0, 0, DateTimeKind.Utc),
                Status = "Inactive",
                CreatedAt = baseDate,
                UpdatedAt = baseDate
            },
            new Employee
            {
                Id = 11,
                FirstName = "Divya",
                LastName = "Bhat",
                Email = "divya.bhat@xyz.com",
                Phone = "9210987654",
                Department = "Engineering",
                Designation = "Frontend Developer",
                Salary = 770000m,
                JoinDate = new DateTime(2022, 10, 11, 0, 0, 0, DateTimeKind.Utc),
                Status = "Active",
                CreatedAt = baseDate,
                UpdatedAt = baseDate
            },
            new Employee
            {
                Id = 12,
                FirstName = "Suresh",
                LastName = "Yadav",
                Email = "suresh.yadav@xyz.com",
                Phone = "9109876543",
                Department = "HR",
                Designation = "Recruiter",
                Salary = 450000m,
                JoinDate = new DateTime(2021, 4, 22, 0, 0, 0, DateTimeKind.Utc),
                Status = "Active",
                CreatedAt = baseDate,
                UpdatedAt = baseDate
            },
            new Employee
            {
                Id = 13,
                FirstName = "Pooja",
                LastName = "Desai",
                Email = "pooja.desai@xyz.com",
                Phone = "9098765432",
                Department = "Marketing",
                Designation = "Content Strategist",
                Salary = 600000m,
                JoinDate = new DateTime(2020, 2, 17, 0, 0, 0, DateTimeKind.Utc),
                Status = "Inactive",
                CreatedAt = baseDate,
                UpdatedAt = baseDate
            },
            new Employee
            {
                Id = 14,
                FirstName = "Ajay",
                LastName = "Kulkarni",
                Email = "ajay.kulkarni@xyz.com",
                Phone = "9987654321",
                Department = "Finance",
                Designation = "Auditor",
                Salary = 730000m,
                JoinDate = new DateTime(2019, 6, 30, 0, 0, 0, DateTimeKind.Utc),
                Status = "Active",
                CreatedAt = baseDate,
                UpdatedAt = baseDate
            },
            new Employee
            {
                Id = 15,
                FirstName = "Neha",
                LastName = "Gupta",
                Email = "neha.gupta@xyz.com",
                Phone = "9876501234",
                Department = "Operations",
                Designation = "Operations Executive",
                Salary = 520000m,
                JoinDate = new DateTime(2023, 3, 5, 0, 0, 0, DateTimeKind.Utc),
                Status = "Active",
                CreatedAt = baseDate,
                UpdatedAt = baseDate
            });
    }
}
