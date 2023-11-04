using PrimeiraAPI.DTOs;
using PrimeiraAPI.Model;

namespace PrimeiraAPI.Service.Interface
{
    public interface IProductsService
    {
        Task<ResponseBase<ProductDTO>> GetDepartaments();
        Task<Product> GetProcuctById(int id);
        Task<Product> Update(Product product);
        Task Delete(Product product);
        Task<Product> Create(Product product);
        Task<List<Product>> ImportExcel(IFormFile file);
        Task<IEnumerable<Product>> SaveExcel(Task<List<Product>> list);
    }
}
