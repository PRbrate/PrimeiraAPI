using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimeiraAPI.Data;
using PrimeiraAPI.Data.Repository;
using PrimeiraAPI.Data.Repository.Interface;
using PrimeiraAPI.DTOs;
using PrimeiraAPI.Model;

namespace PrimeiraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentsController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IDepartamentsRepository _departamentsRepository;
        public DepartamentsController(DepartamentsRepository departamentsRepository)
        {
            _departamentsRepository = departamentsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartaments()
        {
            var response = await _departamentsRepository.GetDepartaments();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public  async Task<DepartmentDTO> GetDepartanebtId(int id)
        {
            Department departament = await _departamentsRepository.GetDepartamentById(id);
            DepartmentDTO departmentDTO = new DepartmentDTO()
            {
                Name = departament.Name, 
                Description = departament.Description,
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
            await _departamentsRepository.Create(department);
            return departamentDTO;

        }

        [HttpPut("{id}")]

        public async Task<DepartmentDTO> UpdateDepartament(DepartmentDTO departamentDTO, int id)
        {

            Department department = await _departamentsRepository.GetDepartamentById(id);
            
            if (id == department.Id)
            {
                await _departamentsRepository.Update(department);
            }
            return departamentDTO;
        }

        [HttpDelete("{id}")]
        public async Task DeleteDepartament(int id)
        {
            Department departament = await _databaseContext.Departaments.FindAsync(id);
            await _departamentsRepository.Delete(departament);
        }
    }
}
