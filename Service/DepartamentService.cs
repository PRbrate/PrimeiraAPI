using PrimeiraAPI.Data.Repository.Interface;
using PrimeiraAPI.DTOs;
using PrimeiraAPI.Model;
using PrimeiraAPI.Service.Interface;

namespace PrimeiraAPI.Service
{
    public class DepartamentService : IDepartamentService
    {
        private readonly IDepartamentsRepository _departamentsRepository;

        public DepartamentService(IDepartamentsRepository departamentsRepository)
        {
            _departamentsRepository = departamentsRepository;
        }

        public async Task<Department> Create(Department department)
        {
            return await _departamentsRepository.Create(department);
        }

        public async Task Delete(Department department)
        {
            await _departamentsRepository.Delete(department);
        }

        public async Task<Department> GetDepartamentById(int id)
        {
            return await _departamentsRepository.GetDepartamentById(id);
        }

        public async Task<ResponseBase<DepartmentDTO>> GetDepartaments()
        {
            return await _departamentsRepository.GetDepartaments();
        }

        public async Task<Department> Update(Department department)
        {
            return await _departamentsRepository.Update(department);
        }
    }
}
