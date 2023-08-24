using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimeiraAPI.Data;
using PrimeiraAPI.DTOs;
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
        public async Task<List<EmployeeDTO>> GetEmployees()
        {
            List<Employee> employees = await _databaseContext.Employees.Include(e=>e.Departament).ToListAsync();
            List<EmployeeDTO> employeeDTOs = new List<EmployeeDTO>();

            foreach (var employee in employees)
            {
                var department = _databaseContext.Departaments.FirstOrDefault(d =>d.Id==employee.DepatmentId);
                var employeeDTO = new EmployeeDTO(
                    employee.Id
                    , employee.Cpf
                    , employee.Name
                    , employee.DateNasc
                    , employee.DepatmentId
                    , department?.Name
                    );
                employeeDTOs.Add(employeeDTO);
            }

            //employeeDTOs = employees.Select(employee => new EmployeeDTO(
            //        employee.Id
            //        , employee.Cpf
            //        , employee.Name
            //        , employee.DateNasc
            //        , employee.DepatmentId
            //        , employee.Departament?.Name
            //        )).ToList();

            return employeeDTOs;
        }

        [HttpGet("{id}")]
        public async Task<EmployeeDTO> GetEmployees(int id)
        {
            Employee func = await _databaseContext.Employees.FindAsync(id);
            EmployeeDTO employeeDTO = new EmployeeDTO(
                func.Id,
                func.Cpf,
                func.Name,
                func.DateNasc,
                func.DepatmentId,
                func.Departament?.Name
                );
            return employeeDTO;
        }

        [HttpPost]
        public EmployeeDTO CreateEmployees([FromBody] EmployeeDTO employeeDTO)
        {
            Employee employee = new Employee()
            {
                Cpf = employeeDTO.Cpf,
                Name = employeeDTO.Name,
                DateNasc = employeeDTO.DateNasc,
                DepatmentId = employeeDTO.DepatmentId,
            };
            _databaseContext.Employees.Add(employee);
            _databaseContext.SaveChangesAsync();
            return employeeDTO;
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
