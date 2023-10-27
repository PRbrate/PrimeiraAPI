using PrimeiraAPI.Data.Filters;
using PrimeiraAPI.DTOs;
using PrimeiraAPI.Model;

namespace PrimeiraAPI.Service.Interface
{
    public interface IEmployeeService
    {
        Task<Employee> Update(Employee employee);
        Task Delete(Employee employee);
        Task<Employee> Create(Employee employee);
        Task<ResponseBase<EmployeeDTO>> GetEmployees(EmployeeFilter employeeFilter);
        Task<Employee> GetEmployeeId(int id);
    }
}
