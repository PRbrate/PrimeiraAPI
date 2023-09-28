using Microsoft.EntityFrameworkCore;
using PrimeiraAPI.Data.Repository.Interface;
using PrimeiraAPI.DTOs;
using PrimeiraAPI.Model;

namespace PrimeiraAPI.Data.Repository
{
    public class DepartamentsRepository : IDepartamentsRepository
    {
        private readonly DatabaseContext _dbContext;

        public DepartamentsRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Department> Create(Department department)
        {
            _dbContext.Departaments.Add(department);
            await _dbContext.SaveChangesAsync();

            return department;
        }

        public async Task Delete(Department department)
        {
            _dbContext.Departaments.Remove(department);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Department> GetDepartamentById(int id)
        {
            var department = await _dbContext.Departaments.FirstOrDefaultAsync(d => d.Id == id);
            if(department is null)
            {
                throw new Exception("Departamento não encontrado");
            };

            return department;
        }

        public async Task<ResponseBase<DepartmentDTO>> GetDepartaments()
        {
            List<Department> departments = await _dbContext.Departaments.ToListAsync();
            List<DepartmentDTO> departmentDTOs = new List<DepartmentDTO>();

            departmentDTOs = departments.Select(department => new DepartmentDTO(
                department.Id,
                department.Name,
                department.Description
                )).ToList();

            var response = new ResponseBase<DepartmentDTO>
            {
                Items = departmentDTOs,
                TotalItems = departmentDTOs.Count
            };

            return response;
        }

        public async Task<Department> Update(Department department)
        {
             
            _dbContext.Departaments.Update(department);
            await _dbContext.SaveChangesAsync();

            return department;
        }
    }
}
