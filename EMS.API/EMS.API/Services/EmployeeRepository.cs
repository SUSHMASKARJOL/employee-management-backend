using EMS.API.Data;
using EMS.API.DTOs;
using EMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EMS.API.Services;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _db;

    public EmployeeRepository(AppDbContext db)
    {
        _db = db;
    }

    public Task<Employee?> GetByIdAsync(int id) =>
        _db.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);

    public async Task<PagedResult<Employee>> GetAllAsync(EmployeeQueryParams queryParams)
    {
        IQueryable<Employee> query = _db.Employees.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(queryParams.Search))
        {
            var term = $"%{queryParams.Search.Trim()}%";
            query = query.Where(e =>
                EF.Functions.Like(e.FirstName + " " + e.LastName, term) ||
                EF.Functions.Like(e.Email, term));
        }

        if (!string.IsNullOrWhiteSpace(queryParams.Department))
        {
            query = query.Where(e => e.Department == queryParams.Department);
        }

        if (!string.IsNullOrWhiteSpace(queryParams.Status))
        {
            query = query.Where(e => e.Status == queryParams.Status);
        }

        var sortDesc = string.Equals(queryParams.SortDir, "desc", StringComparison.OrdinalIgnoreCase);
        query = (queryParams.SortBy ?? "name").ToLowerInvariant() switch
        {
            "salary" => sortDesc ? query.OrderByDescending(e => e.Salary) : query.OrderBy(e => e.Salary),
            "joindate" => sortDesc ? query.OrderByDescending(e => e.JoinDate) : query.OrderBy(e => e.JoinDate),
            _ => sortDesc
               ? query.OrderByDescending(e => e.FirstName).ThenByDescending(e => e.LastName)
               : query.OrderBy(e => e.FirstName).ThenBy(e => e.LastName)
        };

        var totalCount = await query.CountAsync();
        var pageSize = Math.Clamp(queryParams.PageSize <= 0 ? 10 : queryParams.PageSize, 1, 100);
        var totalPages = totalCount == 0 ? 0 : (int)Math.Ceiling(totalCount / (double)pageSize);
        var page = Math.Max(queryParams.Page, 1);
        if (totalPages > 0 && page > totalPages)
        {
            page = totalPages;
        }

        var data = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Employee>
        {
            Data = data,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize,
            TotalPages = totalPages,
            HasNextPage = totalPages > 0 && page < totalPages,
            HasPrevPage = totalPages > 0 && page > 1
        };
    }

    public async Task<Employee> AddAsync(Employee employee)
    {
        employee.CreatedAt = DateTime.UtcNow;
        employee.UpdatedAt = DateTime.UtcNow;
        employee.JoinDate = DateTime.SpecifyKind(employee.JoinDate, DateTimeKind.Utc);

        _db.Employees.Add(employee);
        await _db.SaveChangesAsync();
        return employee;
    }

    public async Task<Employee> UpdateAsync(Employee employee)
    {
        employee.UpdatedAt = DateTime.UtcNow;
        employee.JoinDate = DateTime.SpecifyKind(employee.JoinDate, DateTimeKind.Utc);

        _db.Employees.Update(employee);
        await _db.SaveChangesAsync();
        return employee;
    }

    public async Task DeleteAsync(Employee employee)
    {
        _db.Employees.Remove(employee);
        await _db.SaveChangesAsync();
    }

    public Task<bool> EmailExistsAsync(string email, int? excludeId = null)
    {
        var normalizedEmail = email.Trim().ToLowerInvariant();
        return _db.Employees.AnyAsync(e =>
            e.Email.ToLower() == normalizedEmail &&
            (!excludeId.HasValue || e.Id != excludeId.Value));
    }

    public async Task<DashboardSummaryDto> GetDashboardSummaryAsync()
    {
        var totalEmployees = await _db.Employees.CountAsync();
        var activeCount = await _db.Employees.CountAsync(e => e.Status == "Active");
        var inactiveCount = totalEmployees - activeCount;
        var totalDepartments = await _db.Employees.Select(e => e.Department).Distinct().CountAsync();

        var departmentBreakdown = await _db.Employees
            .GroupBy(e => e.Department)
            .Select(group => new DepartmentBreakdownDto
            {
                Department = group.Key,
                Count = group.Count(),
                Percentage = totalEmployees == 0 ? 0 : Math.Round(group.Count() * 100d / totalEmployees, 1)
            })
            .OrderBy(item => item.Department)
            .ToListAsync();

        var recentEmployees = await _db.Employees
            .AsNoTracking()
            .OrderByDescending(e => e.CreatedAt)
            .ThenByDescending(e => e.Id)
            .Take(5)
            .Select(e => new EmployeeResponseDto
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                Phone = e.Phone,
                Department = e.Department,
                Designation = e.Designation,
                Salary = e.Salary,
                JoinDate = e.JoinDate,
                Status = e.Status,
                CreatedAt = e.CreatedAt,
                UpdatedAt = e.UpdatedAt
            })
            .ToListAsync();

        return new DashboardSummaryDto
        {
            TotalEmployees = totalEmployees,
            ActiveCount = activeCount,
            InactiveCount = inactiveCount,
            TotalDepartments = totalDepartments,
            DepartmentBreakdown = departmentBreakdown,
            RecentEmployees = recentEmployees
        };
    }
}
