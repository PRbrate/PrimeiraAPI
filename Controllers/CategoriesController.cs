using Microsoft.AspNetCore.Mvc;
using PrimeiraAPI.Model;
using PrimeiraAPI.Service.Interface;

namespace PrimeiraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly ICategoriesService _categoryService;

        public CategoriesController(ICategoriesService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGategory()
        {
            var category = await _categoryService.GetCategory();
            return Ok(category);
        }

        [HttpGet("{id}")]
        public async Task<Category> GetCategoryId(int Id)
        {
            var category = await _categoryService.GetCategoryById(Id);
            return category;
        }

        [HttpPost]
        public async Task<Category> CreateCategory([FromBody]Category category)
        {
            await _categoryService.Create(category);
            return category;
        }

        [HttpPut("{id}")]
        public async Task <Category> UpdateCategory(Category category, int id)
        {
            if (id == category.Id)
            {
                await _categoryService.Update(category);
            }

            return category;
        }

        [HttpDelete("{id}")]

        public async Task<Category> DeleteCategory(int id)
        {
            Category category = await _categoryService.GetCategoryById(id);
            await _categoryService.Delete(category);
            return category;
        }
    }
}
