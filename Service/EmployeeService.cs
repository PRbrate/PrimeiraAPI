using PrimeiraAPI.Data.Filters;
using PrimeiraAPI.Data.Repository.Interface;
using PrimeiraAPI.DTOs;
using PrimeiraAPI.Model;
using PrimeiraAPI.Service.Interface;

namespace PrimeiraAPI.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee> Create(Employee employee)
        {
            return await _employeeRepository.Create(employee);
            
        }

        public async Task Delete(Employee employee)
        {
            await _employeeRepository.Delete(employee);
        }

        public async Task<Employee> GetEmployeeId(int id)
        {
            return await _employeeRepository.GetEmployeeId(id);
        }

        public async Task<ResponseBase<EmployeeDTO>> GetEmployees(EmployeeFilter employeeFilter)
        {
            return await _employeeRepository.GetEmployees(employeeFilter);
        }

        public async Task<Employee> Update(Employee employee)
        {
            return await _employeeRepository.Update(employee); 
        }
        

    }
}
