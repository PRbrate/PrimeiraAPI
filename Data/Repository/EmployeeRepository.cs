using Microsoft.EntityFrameworkCore;
using PrimeiraAPI.Data.Filters;
using PrimeiraAPI.Data.Repository.Interface;
using PrimeiraAPI.DTOs;
using PrimeiraAPI.Model;
using PrimeiraAPI.Model.Enums;
using System;

namespace PrimeiraAPI.Data.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {

        private readonly DatabaseContext _dbContext;
        public EmployeeRepository(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }
        public async Task<Employee> Create(Employee employee)
        {
            employee.CountAge();
            employee.UpdateOffice();
            _dbContext.Employees.Add(employee);
            await _dbContext.SaveChangesAsync();

            return employee;
        }

        public async Task Delete(Employee employee)
        {
            _dbContext.Employees.Remove(employee);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Employee> GetEmployeeId(int id)
        {
            var func = await _dbContext.Employees.FirstOrDefaultAsync(e=> e.Id == id);
            if (func is null) 
            {
                throw new Exception("Funcionário não encontrato");
            }
            return func;
            
        }

        public async Task<ResponseBase<EmployeeDTO>> GetEmployees(EmployeeFilter employeeFilter)
        {
            var employees = _dbContext.Employees.Include(e => e.Departament).AsQueryable();
            if (employeeFilter.DepartmentId.HasValue)
            {
                employees = employees.Where(e => e.DepartmentId == employeeFilter.DepartmentId);
            }
            if (!string.IsNullOrWhiteSpace(employeeFilter.Name))
            {
                employees = employees.Where(e => e.Name.Contains(employeeFilter.Name));
            }
            if (employeeFilter.MinSalary.HasValue && employeeFilter.MaxSalary.HasValue)
            {
                employees =  employees.Where(e => e.Salary >= employeeFilter.MinSalary.Value && e.Salary <= employeeFilter.MaxSalary.Value);
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


            var response = new ResponseBase<EmployeeDTO>
            {
                TotJAprendiz = employeeDTOs.Count(e => e.OfficeId == Office.JovemAprendiz),
                TotCLT = employeeDTOs.Count(e => e.OfficeId == Office.CLT),
                TotPJ = employeeDTOs.Count(e => e.OfficeId == Office.PJ),
                Items = employeeDTOs,
                TotalItems = employeeDTOs.Count
            };

            return await Task.FromResult(response);
        }

        public async Task<Employee> Update(Employee employee)
        {
            _dbContext.Employees.Update(employee);
            await _dbContext.SaveChangesAsync();
            return employee;
        }
    }
}
