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
        public async Task<List<EmployeeDTO>> GetEmployees(int? departmentId)
        {
            List<Employee> employees = await _databaseContext.Employees.ToListAsync();
            if (departmentId.HasValue)
            {
                employees = employees.Where(e => e.DepartmentId == departmentId).ToList();
            }

            List<EmployeeDTO> employeeDTOs = new List<EmployeeDTO>();

            foreach (var employee in employees)
            {
                var department = _databaseContext.Departaments.FirstOrDefault(d => d.Id == employee.DepartmentId);
                var employeeDTO = new EmployeeDTO(
                    employee.Id
                    , employee.Cpf
                    , employee.Name
                    , employee.DateNasc
                    , employee.DepartmentId
                    , department?.Name
                    , employee.Salary
                    , employee.Age
                    , employee.OfficeId
         
        
                    );
                employeeDTOs.Add(employeeDTO);
            }

            return employeeDTOs;
        }

        [HttpGet("Salary")]
        public async Task<List<EmployeeDTO>> GetEmployeesbySalary(int? departmentId)
        {
            List<Employee> employees = await _databaseContext.Employees.ToListAsync();

            employees = await _databaseContext.Employees.ToListAsync();
            employees = employees.Where(e => e.Salary >= 1000 & e.Salary <= 4000).ToList();


            List<EmployeeDTO> employeeDTOs = new List<EmployeeDTO>();

            foreach (var employee in employees)
            {
                var department = _databaseContext.Departaments.FirstOrDefault(d => d.Id == employee.DepartmentId);
                var employeeDTO = new EmployeeDTO(
                    employee.Id
                    , employee.Cpf
                    , employee.Name
                    , employee.DateNasc
                    , employee.DepartmentId
                    , department?.Name
                    , employee.Salary
                    , employee.Age
                    , employee.OfficeId
                    );
                employeeDTOs.Add(employeeDTO);
            }

            return employeeDTOs;
        }

        [HttpGet("search")]
        public async Task<EmployeeDTO> GetByName(string name)
        {

            Employee employee = await _databaseContext.Employees.FirstOrDefaultAsync(e => e.Name == name);
            EmployeeDTO employeeDTO = new EmployeeDTO
               (
                   employee.Id,
                   employee.Cpf,
                   employee.Name,
                   employee.DateNasc,
                   employee.DepartmentId,
                   employee.Departament?.Name,
                   employee.Salary,
                   employee.Age,
                   employee.OfficeId
               );
            return employeeDTO;
        }


        [HttpGet("{id}")]
        public async Task<EmployeeDTO> GetEmployeesForId(int id)
        {
            Employee func = await _databaseContext.Employees.FindAsync(id);
            EmployeeDTO employeeDTO = new EmployeeDTO(
                func.Id,
                func.Cpf,
                func.Name,
                func.DateNasc,
                func.DepartmentId,
                func.Departament.Name,
                func.Salary,
                func.Age,
                func.OfficeId
                );
            return employeeDTO;
        }

        [HttpPost]
        public async Task<EmployeeDTO> CreateEmployees([FromBody] EmployeeDTO employeeDTO)
        {

            Employee employee = new Employee()
            {
                Cpf = employeeDTO.Cpf,
                Name = employeeDTO.Name,
                DateNasc = employeeDTO.DateNasc,
                DepartmentId = employeeDTO.DepatmentId,
                Salary = employeeDTO.Salary,
                Age = employeeDTO.Age,
                OfficeId = employeeDTO.OfficeId
            };
            _databaseContext.Employees.Add(employee);
            await _databaseContext.SaveChangesAsync();
            return employeeDTO;
        }

        [HttpPut("{id}")]
        public async Task<Employee> employee(Employee funcionary, int id)
        {
            Employee employee = _databaseContext.Employees.FirstOrDefault(d => d.Id == id);

            if (id == employee.Id)
            {
                _databaseContext.Employees.Update(employee);
                await _databaseContext.SaveChangesAsync();
            }
            return employee;
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
