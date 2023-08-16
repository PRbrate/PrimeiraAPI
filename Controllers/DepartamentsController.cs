using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimeiraAPI.Data;
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
        public async Task<List<Departament>> GetDepartaments() 
        {
            return await _databaseContext.Departaments.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<Departament> GetDepartanebtId(int id)
        {
            Departament departament = await _databaseContext.Departaments.FindAsync(id);
            return departament;
        }

        [HttpPost]
        public Departament CreateDepartament([FromBody] Departament departament)
        {
            _databaseContext.Departaments.Add(departament);
            _databaseContext.SaveChangesAsync();
            return departament;
        }

        [HttpPut("{id}")]

        public Departament UpdateDepartament(Departament departament, int id)
        {
            if (id == departament.Id)
            {
                _databaseContext.Departaments.Update(departament);
                _databaseContext.SaveChangesAsync();
            }
            return departament;
        }

        [HttpDelete("{id}")]
        public async Task<Departament> DeleteDepartament(int id)
        {
            Departament departament = await _databaseContext.Departaments.FindAsync(id);
            _databaseContext.Departaments.Remove(departament);
            await _databaseContext.SaveChangesAsync();
            return departament;
        }
    }
}
