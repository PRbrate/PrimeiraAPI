using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimeiraAPI.Data;
using PrimeiraAPI.Model;

namespace PrimeiraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;
        public EmployeesController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        [HttpGet]
        public async Task<List<Employee>> GetEmployees() 
        { 
            return await _databaseContext.Employees.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<Employee> GetEmployees(int id)
        {
            Employee func = await _databaseContext.Employees.FindAsync(id);
            return func;
        }

        [HttpPost]
        public Employee CreateEmployees(Employee funcionary)
        {
            _databaseContext.Employees.Add(funcionary);
            _databaseContext.SaveChangesAsync();
            return funcionary;
        }

        [HttpPut("{id}")]
        public Employee employee(Employee funcionary, int id)
        {
            if (id == funcionary.Id)
            {
                _databaseContext.Employees.Update(funcionary);
                _databaseContext.SaveChanges();
            }
            return funcionary;
        }

        [HttpDelete("{id}")]
        public async Task<Employee> DeleteEmployee(int id)
        {
            Employee func = await _databaseContext.Employees.FindAsync(id);
            _databaseContext.Employees.Remove(func);
            await _databaseContext.SaveChangesAsync();
            return func;
        }
    }
}
