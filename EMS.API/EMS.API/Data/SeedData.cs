using EMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EMS.API.Data;

public static class SeedData
{
    public static async Task InitializeAsync(AppDbContext db)
    {
        if (await db.Users.AnyAsync())
        {
            return;
        }

        db.Users.AddRange(
            new AppUser
            {
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123", workFactor: 12),
                Role = "Admin",
                CreatedAt = DateTime.UtcNow
            },
            new AppUser
            {
                Username = "viewer",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("viewer123", workFactor: 12),
                Role = "Viewer",
                CreatedAt = DateTime.UtcNow
            });

        await db.SaveChangesAsync();
    }
}
