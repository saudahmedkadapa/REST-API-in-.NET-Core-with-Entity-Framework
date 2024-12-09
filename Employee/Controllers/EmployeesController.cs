using Employee.Data;
using Employee.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet(Name ="GetAll")]
        public List <Models.Employee> GetAllEmployee()
        {
            var allemployee= dbContext.Employees.ToList();
            return  allemployee;
           
        }

        [HttpPost(Name = "AddEmp")]

        public Models.Employee AddEmployee(Models.Employee employee)
        {
            dbContext.Employees.Add(employee);
            dbContext.SaveChanges();
            return employee;
        }


        [HttpDelete("{id}")]
        public bool DeleteEmployee(int id)
        {
            Models.Employee employee = null;
            foreach (var emp in dbContext.Employees)
            {
                if (emp.id == id) 
                {
                    employee = emp;
                    break;
                }
            }
            if (employee == null)
            {
                return false; 
            }

            
            dbContext.Employees.Remove(employee);
            dbContext.SaveChanges();
            return true; 
        }


        [HttpPut("{id}")]
        public async Task PutEmployee(int id, Models.Employee employee)
        {
            if (id != employee.id)
            {
                throw new ArgumentException("The provided ID does not match the ID in the request body.");
            }

            var existingEmployee = await dbContext.Employees.FirstOrDefaultAsync(e => e.id == id);
            if (existingEmployee == null)
            {
                throw new KeyNotFoundException("Employee not found.");
            }

            existingEmployee.name = employee.name;
            existingEmployee.phoneNo = employee.phoneNo;
            existingEmployee.salary = employee.salary;

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("An error occurred while updating the employee.");
            }
        }






    }
}
