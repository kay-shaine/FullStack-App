using FullStack.API.Data;
using FullStack.API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullStack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly FullStackDbContext _fullStackDbContext;

        public EmployeesController(FullStackDbContext fullStackDbContext)
        {
            _fullStackDbContext = fullStackDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _fullStackDbContext.Employees.ToListAsync();
            return Ok(employees);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employee = await _fullStackDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if(employee == null) 
            {
                return NotFound(); 
            }

            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody]Employee employeeRequest)
        {
            employeeRequest.Id = Guid.NewGuid();
            await _fullStackDbContext.Employees.AddAsync(employeeRequest);
            await _fullStackDbContext.SaveChangesAsync();
            return Ok(employeeRequest);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee( Guid id, Employee UpdateEmployeeRequest)
        {
            var employee = await _fullStackDbContext.Employees.FindAsync(id);

            if(employee == null)
            { 
                return NotFound(); 
            }

            employee.Name = UpdateEmployeeRequest.Name;
            employee.Email = UpdateEmployeeRequest.Email;
            employee.Phone = UpdateEmployeeRequest.Phone;
            employee.Price = UpdateEmployeeRequest.Price;
            employee.Department = UpdateEmployeeRequest.Department;

            await _fullStackDbContext.SaveChangesAsync();

            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var employee = await _fullStackDbContext.Employees.FindAsync(id);

            if(employee == null)
            { 
                return NotFound();
            }

             _fullStackDbContext.Employees.Remove(employee);
            await _fullStackDbContext.SaveChangesAsync();

            return Ok(employee);
        }
    }
}
