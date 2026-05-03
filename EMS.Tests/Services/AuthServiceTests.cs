using EMS.API.Data;
using EMS.API.DTOs;
using EMS.API.Models;
using EMS.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Moq;

namespace EMS.Tests.Services;

[TestFixture]
public class AuthServiceTests
{
    private AppDbContext _db = null!;
    private AuthService _service = null!;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _db = new AppDbContext(options);
        _db.Users.Add(new AppUser
        {
            Username = "admin",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
            Role = "Admin",
            CreatedAt = DateTime.UtcNow
        });
        _db.SaveChanges();

        var config = new Mock<IConfiguration>();
        config.Setup(c => c["Jwt:Key"]).Returns("TestSecretKey_32Chars_LongEnough!!");
        config.Setup(c => c["Jwt:Issuer"]).Returns("EMS.API");
        config.Setup(c => c["Jwt:Audience"]).Returns("EMS.Client");
        config.Setup(c => c["Jwt:ExpiryHours"]).Returns("8");

        _service = new AuthService(_db, config.Object);
    }

    [Test]
    public async Task LoginAsync_ValidCredentials_ReturnsToken()
    {
        var result = await _service.LoginAsync(new LoginRequestDto { Username = "admin", Password = "admin123" });

        Assert.That(result.Success, Is.True);
        Assert.That(result.Token, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public async Task LoginAsync_WrongPassword_ReturnsFailure()
    {
        var result = await _service.LoginAsync(new LoginRequestDto { Username = "admin", Password = "wrong" });

        Assert.That(result.Success, Is.False);
    }

    [Test]
    public async Task RegisterAsync_DuplicateUsername_ReturnsFailure()
    {
        var result = await _service.RegisterAsync(new RegisterRequestDto
        {
            Username = "admin",
            Password = "admin123",
            Role = "Admin"
        });

        Assert.That(result.Success, Is.False);
    }
}
