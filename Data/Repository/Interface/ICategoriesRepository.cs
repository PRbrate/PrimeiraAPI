using PrimeiraAPI.DTOs;
using PrimeiraAPI.Model;

namespace PrimeiraAPI.Data.Repository.Interface
{
    public interface ICategoriesRepository
    {
        Task<ResponseBase<Category>> GetCategory();
        Task<Category> GetCategoryById(int id);
        Task<Category> Update(Category category);
        Task Delete(Category category);
        Task<Category> Create(Category category);
    }
}
