using EMS.API.Controllers;
using EMS.API.DTOs;
using EMS.API.Models;
using EMS.API.Services;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Moq;

namespace EMS.Tests.Controllers;

[TestFixture]
public class EmployeesControllerTests
{
    [Test]
    public async Task GetById_ExistingEmployee_ReturnsOkResult()
    {
        var repoMock = new Mock<IEmployeeRepository>();
        repoMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(new Employee
        {
            Id = 1,
            FirstName = "Priya",
            LastName = "Menon",
            Email = "priya@xyz.com",
            Phone = "9876543210",
            Department = "Engineering",
            Designation = "Engineer",
            Salary = 600000m,
            JoinDate = DateTime.UtcNow,
            Status = "Active"
        });

        var service = new EmployeeService(repoMock.Object);
        var controller = new EmployeesController(service);

        var result = await controller.GetById(1);

        Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
    }
}
