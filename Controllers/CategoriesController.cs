using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimeiraAPI.Data;
using PrimeiraAPI.Model;

namespace PrimeiraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;

        public CategoriesController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        [HttpGet]
        public async Task<List<Category>> GetGategory()
        {
            return await _databaseContext.Categories.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<Category> GetCategoryId(int Id)
        {
            Category category = await _databaseContext.Categories.FindAsync(Id);
            return category;
        }

        [HttpPost]
        public Category CreateCategory(Category category)
        {
            _databaseContext.Categories.Add(category);
            _databaseContext.SaveChangesAsync();
            return category;
        }

        [HttpPut("{id}")]
        public Category UpdateCategory(Category category, int id)
        {
            if (id == category.Id)
            {
                _databaseContext.Categories.Update(category);
                _databaseContext.SaveChangesAsync();
            }

            return category;
        }

        [HttpDelete("{id}")]

        public async Task<Category> DeleteCategory(int id)
        {
            Category category = await _databaseContext.Categories.FindAsync(id);
            _databaseContext.Categories.Remove(category);
            await _databaseContext.SaveChangesAsync();
            return category;
        }
    }
}
