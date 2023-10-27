using PrimeiraAPI.Data.Repository.Interface;
using PrimeiraAPI.Model;
using PrimeiraAPI.Service.Interface;

namespace PrimeiraAPI.Service
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository _categoryRepository;

        public CategoriesService(ICategoriesRepository categoryrepository)
        {
            _categoryRepository = categoryrepository;
        }


        public async Task<Category> Create(Category category)
        {
            return await _categoryRepository.Create(category);
        }

        public async Task Delete(Category category)
        {
            await _categoryRepository.Delete(category);
        }

        public async Task<ResponseBase<Category>> GetCategory()
        {
            return await _categoryRepository.GetCategory();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await _categoryRepository.GetCategoryById(id);
        }

        public async Task<Category> Update(Category category)
        {
            return await _categoryRepository.Update(category);
        }
    }
}
