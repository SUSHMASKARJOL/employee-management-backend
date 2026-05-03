using EMS.API.DTOs;
using EMS.API.Models;

namespace EMS.API.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _repo;

        public EmployeeService(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public async Task<EmployeeResponseDto?> GetByIdAsync(int id)
        {
            var emp = await _repo.GetByIdAsync(id);
            return emp is null ? null : MapToDto(emp);
        }

        public async Task<PagedResult<EmployeeResponseDto>> GetAllAsync(EmployeeQueryParams queryParams)
        {
            var result = await _repo.GetAllAsync(queryParams);
            return new PagedResult<EmployeeResponseDto>
            {
                Data = result.Data.Select(MapToDto),
                TotalCount = result.TotalCount,
                Page = result.Page,
                PageSize = result.PageSize,
                TotalPages = result.TotalPages,
                HasNextPage = result.HasNextPage,
                HasPrevPage = result.HasPrevPage
            };
        }

        public async Task<(EmployeeResponseDto? Dto, string? Error)> CreateAsync(EmployeeRequestDto dto)
        {
            if (await _repo.EmailExistsAsync(dto.Email))
                return (null, "An employee with this email already exists.");

            var emp = MapFromDto(dto);
            var created = await _repo.AddAsync(emp);
            return (MapToDto(created), null);
        }

        public async Task<(EmployeeResponseDto? Dto, string? Error)> UpdateAsync(int id, EmployeeRequestDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing is null)
                return (null, "Employee not found.");

            if (await _repo.EmailExistsAsync(dto.Email, excludeId: id))
                return (null, "Another employee with this email already exists.");

            existing.FirstName = dto.FirstName;
            existing.LastName = dto.LastName;
            existing.Email = dto.Email;
            existing.Phone = dto.Phone;
            existing.Department = dto.Department;
            existing.Designation = dto.Designation;
            existing.Salary = dto.Salary;
            existing.JoinDate = dto.JoinDate;
            existing.Status = dto.Status;

            var updated = await _repo.UpdateAsync(existing);
            return (MapToDto(updated), null);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var emp = await _repo.GetByIdAsync(id);
            if (emp is null) return false;
            await _repo.DeleteAsync(emp);
            return true;
        }

        public async Task<DashboardSummaryDto> GetDashboardAsync()
            => await _repo.GetDashboardSummaryAsync();

        private static EmployeeResponseDto MapToDto(Employee e) => new()
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
        };

        private static Employee MapFromDto(EmployeeRequestDto dto) => new()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Phone = dto.Phone,
            Department = dto.Department,
            Designation = dto.Designation,
            Salary = dto.Salary,
            JoinDate = dto.JoinDate,
            Status = dto.Status
        };
    }
}