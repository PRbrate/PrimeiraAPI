using Microsoft.EntityFrameworkCore;
using PrimeiraAPI.Data.Repository.Interface;
using PrimeiraAPI.Model;

namespace PrimeiraAPI.Data.Repository
{
    public class CategoryRepository : ICategoriesRepository
    {
        private readonly DatabaseContext _dbContext;
        public CategoryRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Category> Create(Category category)
        {
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();

            return category;
        }

        public async Task Delete(Category category)
        {
            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            Category category = await _dbContext.Categories.FindAsync(id);
            return category;
        }

        public async Task<ResponseBase<Category>> GetCategory()
        {
            List<Category> category = await _dbContext.Categories.ToListAsync();


            var response = new ResponseBase<Category>
            {
                Items = category,
                TotalItems = category.Count
            };

            return response;
        }

        public async Task<Category> Update(Category category)
        {
            _dbContext.Categories.Update(category);
            await _dbContext.SaveChangesAsync();

            return category;
        }
    }
}
