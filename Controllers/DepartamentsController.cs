using Microsoft.AspNetCore.Mvc;
using PrimeiraAPI.DTOs;
using PrimeiraAPI.Model;
using PrimeiraAPI.Service.Interface;

namespace PrimeiraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentsController : ControllerBase
    {
        private readonly IDepartamentService _departamentsService;
        public DepartamentsController(IDepartamentService departamentsService)
        {
            _departamentsService = departamentsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartaments()
        {
            var response = await _departamentsService.GetDepartaments();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public  async Task<DepartmentDTO> GetDepartanebtId(int id)
        {
            Department departament = await _departamentsService.GetDepartamentById(id);
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
            await _departamentsService.Create(department);
            return departamentDTO;

        }

        [HttpPut("{id}")]

        public async Task<DepartmentDTO> UpdateDepartament(DepartmentDTO departamentDTO, int id)
        {

            Department department = await _departamentsService.GetDepartamentById(id);
            
            if (id == department.Id)
            {
                await _departamentsService.Update(department);
            }
            return departamentDTO;
        }

        [HttpDelete("{id}")]
        public async Task DeleteDepartament(int id)
        {
            Department departament = await _departamentsService.GetDepartamentById(id);
            await _departamentsService.Delete(departament);
        }
    }
}
