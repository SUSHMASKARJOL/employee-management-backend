using EMS.API.DTOs;
using EMS.API.Models;
using EMS.API.Services;
using NUnit.Framework;
using Moq;

namespace EMS.Tests.Services;

[TestFixture]
public class EmployeeServiceTests
{
    private Mock<IEmployeeRepository> _repoMock = null!;
    private EmployeeService _service = null!;

    [SetUp]
    public void Setup()
    {
        _repoMock = new Mock<IEmployeeRepository>();
        _service = new EmployeeService(_repoMock.Object);
    }

    [Test]
    public async Task GetByIdAsync_ValidId_ReturnsMappedDto()
    {
        _repoMock.Setup(repo => repo.GetByIdAsync(1))
            .ReturnsAsync(new Employee
            {
                Id = 1,
                FirstName = "Priya",
                LastName = "Menon",
                Email = "priya@xyz.com",
                Phone = "9876543210",
                Department = "Engineering",
                Designation = "Engineer",
                Salary = 500000m,
                JoinDate = DateTime.UtcNow,
                Status = "Active"
            });

        var result = await _service.GetByIdAsync(1);

        Assert.That(result, Is.Not.Null);
        Assert.That(result!.FirstName, Is.EqualTo("Priya"));
        _repoMock.Verify(repo => repo.GetByIdAsync(1), Times.Once);
    }

    [Test]
    public async Task GetByIdAsync_InvalidId_ReturnsNull()
    {
        _repoMock.Setup(repo => repo.GetByIdAsync(999)).ReturnsAsync((Employee?)null);

        var result = await _service.GetByIdAsync(999);

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task CreateAsync_NewEmployee_CallsRepository()
    {
        var request = new EmployeeRequestDto
        {
            FirstName = "Neha",
            LastName = "Gupta",
            Email = "neha@xyz.com",
            Phone = "9876501234",
            Department = "Operations",
            Designation = "Executive",
            Salary = 520000m,
            JoinDate = DateTime.UtcNow,
            Status = "Active"
        };

        _repoMock.Setup(repo => repo.EmailExistsAsync(request.Email, null)).ReturnsAsync(false);
        _repoMock.Setup(repo => repo.AddAsync(It.IsAny<Employee>()))
            .ReturnsAsync((Employee employee) =>
            {
                employee.Id = 15;
                return employee;
            });

        var result = await _service.CreateAsync(request);

        Assert.That(result.Dto, Is.Not.Null);
        _repoMock.Verify(repo => repo.AddAsync(It.IsAny<Employee>()), Times.Once);
    }
}
