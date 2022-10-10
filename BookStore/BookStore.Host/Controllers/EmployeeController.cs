using BookStore.BL.Interfaces;
using BookStore.Models.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Host.Controllers
{
    [Authorize]
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Employee>>> Get()
        {
           var result = await _employeeService.GetAllEmployees();

           return Ok(result);
        }


        [HttpGet("GetById")]
        public async Task<ActionResult<IEnumerable<Employee>>> Get(int id)
        {
            var result = await _employeeService.GetEmployeeDetails(id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> Post(Employee employee)
        {
            var result = await _employeeService.AddEmployee(employee);
            return result;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> Put(Employee employee)
        {
           
           await _employeeService.UpdateEmployee(employee);
           return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> Delete(int id)
        {
            await _employeeService.DeleteEmployee(id);

            return Ok();
        }

    }
}
