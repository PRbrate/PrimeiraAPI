using PrimeiraAPI.DTOs;
using PrimeiraAPI.Model;

namespace PrimeiraAPI.Data.Repository.Interface
{
    public interface IProductRepository
    {
        Task<ResponseBase<ProductDTO>> GetDepartaments();
        Task<Product> GetProcuctById(int id);
        Task<Product> Update(Product product);
        Task Delete(Product product);
        Task<Product> Create(Product product);
    }
}
