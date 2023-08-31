using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.ValueGeneration.Internal;
using PrimeiraAPI.Data;
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
        public EmployeesController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees(int? departmentId, string name, int? initialsalary, int? finalSalary)
        {
            List<Employee> employees = await _databaseContext.Employees.Include(e => e.Departament).ToListAsync();
            if (departmentId.HasValue)
            {
                employees = employees.Where(e => e.DepartmentId == departmentId).ToList();
            }
            if (!string.IsNullOrWhiteSpace(name))
            {
                employees = employees.Where(e => e.Name.Contains(name)).ToList();
            }
            if (initialsalary.HasValue && finalSalary.HasValue)
            {
                employees = await _databaseContext.Employees.Where(e => e.Salary >= initialsalary.Value && e.Salary <= finalSalary.Value).ToListAsync();
            }


            List<EmployeeDTO> employeeDTOs = new List<EmployeeDTO>();

            foreach (var employee in employees)
            {

                var employeeDTO = new EmployeeDTO(
                    employee.Id
                    , employee.Cpf
                    , employee.Name
                    , employee.Age
                    , employee.DateNasc
                    , employee.DepartmentId
                    , employee.Departament?.Name
                    , employee.Salary
                    , employee.OfficeId

                    );
                employeeDTOs.Add(employeeDTO);
            }

            var respose = new ResponseBase<EmployeeDTO>
            {
                TotJAprendiz = employeeDTOs.Count(e => e.OfficeId == Office.JovemAprendiz),
                TotCLT = employeeDTOs.Count(e => e.OfficeId == Office.CLT),
                TotPJ = employeeDTOs.Count(e => e.OfficeId == Office.PJ),
                Items = employeeDTOs,
                TotalItems = employeeDTOs.Count
            };

            return Ok(respose);
        }

        //[HttpGet("Salary")]
        //public async Task<List<EmployeeDTO>> GetEmployeesbySalary()
        //{
        //    List<Employee> employees = await _databaseContext.Employees.ToListAsync();

        //    employees = await _databaseContext.Employees.Where(e => e.Salary >= initialsalary & e.Salary <= finalSalary).ToListAsync();

        //    List<EmployeeDTO> employeeDTOs = new List<EmployeeDTO>();

        //    foreach (var employee in employees)
        //    {
        //        var department = _databaseContext.Departaments.FirstOrDefault(d => d.Id == employee.DepartmentId);
        //        var employeeDTO = new EmployeeDTO(
        //            employee.Id
        //            , employee.Cpf
        //            , employee.Name
        //            , employee.DateNasc
        //            , employee.DepartmentId
        //            , department?.Name
        //            , employee.Salary
        //            , employee.OfficeId
        //            );
        //        employeeDTOs.Add(employeeDTO);
        //    }

        //    return employeeDTOs;
        //}

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
                DepartmentId = employeeDTO.DepatmentId,
                Salary = employeeDTO.Salary,
            };
            employee.CountAge();
            employee.UpdateOffice();
            _databaseContext.Employees.Add(employee);
            await _databaseContext.SaveChangesAsync();
            return employeeDTO;
        }

        [HttpPut("{id}")]
        public async Task<Employee> UpdateEmployee(Employee funcionary, int id)
        {
            Employee employee = _databaseContext.Employees.FirstOrDefault(d => d.Id == id);

            if (id == employee.Id)
            {
                employee.Name = funcionary.Name;
                employee.Cpf = funcionary.Cpf;
                employee.DateNasc = funcionary.DateNasc;
                employee.DepartmentId = funcionary.DepartmentId;
                employee.Salary = funcionary.Salary;
                employee.CountAge();
                employee.UpdateOffice();
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
