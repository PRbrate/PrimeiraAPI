using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimeiraAPI.Data;
using PrimeiraAPI.DTOs;
using PrimeiraAPI.Model;

namespace PrimeiraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentsController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;
        public DepartamentsController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        [HttpGet]
        public async Task<List<DepartmentDTO>> GetDepartaments()
        {
            List<Department> departments = await _databaseContext.Departaments.ToListAsync();
            List<DepartmentDTO> departmentDTOs = new List<DepartmentDTO>();

            departmentDTOs = departments.Select(department => new DepartmentDTO(
                department.Id,
                department.Name,
                department.Description
                )).ToList();
            return departmentDTOs;
        }

        [HttpGet("{id}")]
        public  DepartmentDTO GetDepartanebtId(int id)
        {
            Department departament = new Department();
            var department =  _databaseContext.Departaments.FirstOrDefault(d => d.Id == id);
            DepartmentDTO departmentDTO = new DepartmentDTO()
            {
                Name = department.Name, 
                Description = department.Description,
            };
            return departmentDTO;
                
        }

        [HttpPost]
        public async Task<DepartmentDTO> CreateDepartament([FromBody] DepartmentDTO departamentDTO)
        {
            Department department = new Department()
            {
                Name = departamentDTO.Name,
                Description = departamentDTO.Description,
            };
            _databaseContext.Departaments.Add(department);
            await _databaseContext.SaveChangesAsync();
            return departamentDTO;

        }

        [HttpPut("{id}")]

        public async Task<DepartmentDTO> UpdateDepartament(DepartmentDTO departamentDTO, int id)
        {

            Department department = _databaseContext.Departaments.FirstOrDefault(d => d.Id == id);
            
            if (id == department.Id)
            {
                _databaseContext.Departaments.Update(department);
                await _databaseContext.SaveChangesAsync();
            }
            return departamentDTO;
        }

        [HttpDelete("{id}")]
        public async Task<Department> DeleteDepartament(int id)
        {
            Department departament = await _databaseContext.Departaments.FindAsync(id);
            _databaseContext.Departaments.Remove(departament);
            await _databaseContext.SaveChangesAsync();
            return departament;
        }
    }
}
