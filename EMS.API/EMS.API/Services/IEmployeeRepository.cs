using EMS.API.DTOs;
using EMS.API.Models;

namespace EMS.API.Services
{
    public interface IEmployeeRepository
    {
        Task<Employee?> GetByIdAsync(int id);

        Task<PagedResult<Employee>> GetAllAsync(EmployeeQueryParams queryParams);

        Task<Employee> AddAsync(Employee employee);

        Task<Employee> UpdateAsync(Employee employee);

        Task DeleteAsync(Employee employee);

        Task<bool> EmailExistsAsync(string email, int? excludeId = null);

        Task<DashboardSummaryDto> GetDashboardSummaryAsync();
    }
}