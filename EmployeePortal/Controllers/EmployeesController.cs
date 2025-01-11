using EmployeePortal.Data;
using EmployeePortal.Models;
using EmployeePortal.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePortal.Controllers
{
    //localhost:xxxx/api/employees
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeesController(ApplicationDbContext DbContext)
        {
            dbContext = DbContext;
        }
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var employees = dbContext.Employees.ToList();

            return Ok(employees);
        }

        [HttpPost]
        public IActionResult AddEmployee(AddEmployeeDTO objEmployee)
        {
            Employee employee = new Employee()
            {
                Name = objEmployee.Name,
                Email = objEmployee.Email,
                Phone = objEmployee.Phone,
                Salary = objEmployee.Salary
            };
            dbContext.Employees.Add(employee);
            dbContext.SaveChanges();
            return Ok(employee);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployeeById(Guid id)
        {
            var employee = dbContext.Employees.Find(id);
            if(employee is null)
            {
                return NotFound("Could not find the Employee");
            }
            return Ok(employee);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id, UpdateEmployeeDTO updateEmp)
        {
            var employee = dbContext.Employees.Find(id);
            if (employee is null)
            {
                return NotFound("Could not find the Employee");
            }

            employee.Name = updateEmp.Name;
            employee.Email = updateEmp.Email;
            employee.Salary = updateEmp.Salary;
            employee.Phone = updateEmp.Phone;
            dbContext.Employees.Update(employee);
            dbContext.SaveChanges();
            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            var employee = dbContext.Employees.Find(id);
            if (employee is null)
            {
                return NotFound("Could not find the Employee");
            }
            dbContext.Employees.Remove(employee);
            dbContext.SaveChanges();
            return Ok();
        }
    }
}