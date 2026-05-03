using EMS.API.DTOs;
using EMS.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] 
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeService _svc;

        public EmployeesController(EmployeeService svc)
        {
            _svc = svc;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Viewer")]
        public async Task<ActionResult<PagedResult<EmployeeResponseDto>>> GetAll(
            [FromQuery] EmployeeQueryParams queryParams)
        {
            var result = await _svc.GetAllAsync(queryParams);
            return Ok(result);
        }

        [HttpGet("dashboard")]
        [Authorize(Roles = "Admin,Viewer")]
        public async Task<ActionResult<DashboardSummaryDto>> GetDashboard()
        {
            var summary = await _svc.GetDashboardAsync();
            return Ok(summary);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin,Viewer")]
        public async Task<ActionResult<EmployeeResponseDto>> GetById(int id)
        {
            var emp = await _svc.GetByIdAsync(id);
            if (emp is null) return NotFound(new { message = $"Employee with ID {id} not found." });
            return Ok(emp);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<EmployeeResponseDto>> Create(
            [FromBody] EmployeeRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (created, error) = await _svc.CreateAsync(dto);
            if (error is not null)
                return Conflict(new { message = error });

            return CreatedAtAction(nameof(GetById), new { id = created!.Id }, created);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<EmployeeResponseDto>> Update(
            int id, [FromBody] EmployeeRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (updated, error) = await _svc.UpdateAsync(id, dto);

            if (error == "Employee not found.")
                return NotFound(new { message = error });

            if (error is not null)
                return Conflict(new { message = error });

            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _svc.DeleteAsync(id);
            if (!deleted)
                return NotFound(new { message = $"Employee with ID {id} not found." });

            return Ok(new { message = "Employee deleted successfully." });
        }
    }
}