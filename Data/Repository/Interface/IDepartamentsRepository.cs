using PrimeiraAPI.DTOs;
using PrimeiraAPI.Model;

namespace PrimeiraAPI.Data.Repository.Interface
{
    public interface IDepartamentsRepository
    {
        Task<ResponseBase<DepartmentDTO>> GetDepartaments();
        Task<Department> GetDepartamentById(int id);
        Task<Department> Update(Department department);
        Task Delete(Department department);
        Task<Department> Create(Department department);
    }
}
