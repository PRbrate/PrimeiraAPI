using Microsoft.AspNetCore.Mvc;
using PrimeiraAPI.Data.Filters;
using PrimeiraAPI.DTOs;
using PrimeiraAPI.Model;
using PrimeiraAPI.Service.Interface;

namespace PrimeiraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees([FromQuery] EmployeeFilter employeeFilter)
        {           
           var employeeDTOs = await _employeeService.GetEmployees(employeeFilter);
           return Ok(employeeDTOs);
        }

        [HttpGet("{id}")]
        public async Task<EmployeeDTO> GetEmployeesForId(int id)
        {
            var Employee = await _employeeService.GetEmployeeId(id);
            EmployeeDTO employeeDTO = new EmployeeDTO(
                Employee.Id,
                Employee.Cpf,
                Employee.Name,
                Employee.Age,
                Employee.DateNasc,
                Employee.DepartmentId,
                Employee.Departament?.Name,
                Employee.Salary,
                Employee.OfficeId
                );
            return employeeDTO;
        }

        //[HttpGet("UploadOffice")]
        //public async Task<List<EmployeeDTO>> UploadOffice()
        //{
        //    List<Employee> employees = await _databaseContext.Employees.ToListAsync();
        //    List<EmployeeDTO> employeeDTOs = new List<EmployeeDTO>();

        //    foreach (var employee in employees)
        //    {
        //        employee.UpdateOffice();
        //        var employeeDTO = new EmployeeDTO(
        //            employee.Id
        //            , employee.Cpf
        //            , employee.Name
        //            , employee.Age
        //            , employee.DateNasc
        //            , employee.DepartmentId
        //            , employee.Departament?.Name
        //            , employee.Salary
        //            , employee.OfficeId
        //            );
        //        employeeDTOs.Add(employeeDTO);
        //    }
        //    _databaseContext.UpdateRange(employees);
        //    await _databaseContext.SaveChangesAsync();
        //    return employeeDTOs;
        //}

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

            await _employeeService.Create(employee);
            return employeeDTO;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(Employee funcionary, int id)
        { 
            Employee employee = await _employeeService.GetEmployeeId(id);

            if (id == employee.Id)
            {
                employee.Name = funcionary.Name;
                employee.Cpf = funcionary.Cpf;
                employee.DateNasc = funcionary.DateNasc;
                employee.DepartmentId = funcionary.DepartmentId;
                employee.Salary = funcionary.Salary;
                employee.CountAge();
                employee.UpdateOffice();
                await _employeeService.Update(employee);
            }
            return Ok(employee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            Employee employee = await _employeeService.GetEmployeeId(id);
            if(employee != null) 
            {
                await _employeeService.Delete(employee);
            }
            return Ok();
        }
    }
}
