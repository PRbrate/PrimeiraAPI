using PrimeiraAPI.Model;

namespace PrimeiraAPI.Service.Interface
{
    public interface ICategoriesService
    {
        Task<ResponseBase<Category>> GetCategory();
        Task<Category> GetCategoryById(int id);
        Task<Category> Update(Category category);
        Task Delete(Category category);
        Task<Category> Create(Category category);
    }
}
