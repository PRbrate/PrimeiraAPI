using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimeiraAPI.Data;
using PrimeiraAPI.Data.Repository;
using PrimeiraAPI.Model;

namespace PrimeiraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly CategoryRepository _categoryRepository;

        public CategoriesController(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetGategory()
        {
            var category = await _categoryRepository.GetCategory();
            return Ok(category);
        }

        [HttpGet("{id}")]
        public async Task<Category> GetCategoryId(int Id)
        {
            var category = await _categoryRepository.GetCategoryById(Id);
            return category;
        }

        [HttpPost]
        public async Task<Category> CreateCategory([FromBody]Category category)
        {
            await _categoryRepository.Create(category);
            return category;
        }

        [HttpPut("{id}")]
        public async Task <Category> UpdateCategory(Category category, int id)
        {
            if (id == category.Id)
            {
                await _categoryRepository.Update(category);
            }

            return category;
        }

        [HttpDelete("{id}")]

        public async Task<Category> DeleteCategory(int id)
        {
            Category category = await _categoryRepository.GetCategoryById(id);
            await _categoryRepository.Delete(category);
            return category;
        }
    }
}
