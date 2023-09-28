using PrimeiraAPI.Data.Filters;
using PrimeiraAPI.DTOs;
using PrimeiraAPI.Model;

namespace PrimeiraAPI.Data.Repository.Interface
{
    public interface IEmployeeRepository
    {
        Task<ResponseBase<EmployeeDTO>> GetEmployees(EmployeeFilter employeeFilter);
        Task<Employee> GetEmployeeId(int id);
        Task<Employee> Update(Employee employee);
        Task Delete(Employee employee);
        Task<Employee> Create(Employee employee);

    }
}
