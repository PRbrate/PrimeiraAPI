using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.ValueGeneration.Internal;
using PrimeiraAPI.Data;
using PrimeiraAPI.Data.Filters;
using PrimeiraAPI.Data.Repository.Interface;
using PrimeiraAPI.DTOs;
using PrimeiraAPI.Model;
using PrimeiraAPI.Model.Enums;

namespace PrimeiraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees([FromQuery] EmployeeFilter employeeFilter)
        {           
           var employeeDTOs = await _employeeRepository.GetEmployees(employeeFilter);
           return Ok(employeeDTOs);
        }

        [HttpGet("{id}")]
        public async Task<EmployeeDTO> GetEmployeesForId(int id)
        {
            var func = await _employeeRepository.GetEmployeeId(id);
            EmployeeDTO employeeDTO = new EmployeeDTO(
                func.Id,
                func.Cpf,
                func.Name,
                func.DateNasc,
                func.DepartmentId,
                func.Departament.Name,
                func.Salary,
                func.OfficeId
                );
            return employeeDTO;
        }

        [HttpGet("UploadOffice")]
        public async Task<List<EmployeeDTO>> UploadOffice()
        {
            List<Employee> employees = await _databaseContext.Employees.ToListAsync();
            List<EmployeeDTO> employeeDTOs = new List<EmployeeDTO>();

            foreach (var employee in employees)
            {
                employee.UpdateOffice();
                var employeeDTO = new EmployeeDTO(
                    employee.Id
                    , employee.Cpf
                    , employee.Name
                    , employee.DateNasc
                    , employee.DepartmentId
                    , employee.Departament?.Name
                    , employee.Salary
                    , employee.OfficeId
                    );
                employeeDTOs.Add(employeeDTO);
            }
            _databaseContext.UpdateRange(employees);
            await _databaseContext.SaveChangesAsync();
            return employeeDTOs;
        }

        [HttpPost]
        public async Task<EmployeeDTO> CreateEmployees([FromBody] EmployeeDTO employeeDTO)
        {

            Employee employee = new Employee()
            {
                Cpf = employeeDTO.Cpf,
                Name = employeeDTO.Name,
                DateNasc = employeeDTO.DateNasc,
                DepartmentId = employeeDTO.DepartmentId,
                Salary = employeeDTO.Salary,
            };

            await _employeeRepository.Create(employee);
            return employeeDTO;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(Employee funcionary, int id)
        { 
            Employee employee = await _employeeRepository.GetEmployeeId(id);

            if (id == employee.Id)
            {
                employee.Name = funcionary.Name;
                employee.Cpf = funcionary.Cpf;
                employee.DateNasc = funcionary.DateNasc;
                employee.DepartmentId = funcionary.DepartmentId;
                employee.Salary = funcionary.Salary;
                employee.CountAge();
                employee.UpdateOffice();
                await _employeeRepository.Update(employee);
            }
            return Ok(employee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            Employee employee = await _databaseContext.Employees.FindAsync(id);
            await _employeeRepository.Delete(employee);
            return Ok();
        }
    }
}
