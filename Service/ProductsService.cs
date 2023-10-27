using PrimeiraAPI.Data.Repository.Interface;
using PrimeiraAPI.DTOs;
using PrimeiraAPI.Model;
using PrimeiraAPI.Service.Interface;

namespace PrimeiraAPI.Service
{
    public class ProductsService : IProductsService
    {
        private readonly IProductRepository _ProductRepository;
        public ProductsService(IProductRepository productRepository)
        {
            _ProductRepository = productRepository;
        }
        public async Task<Product> Create(Product product)
        {
            return await _ProductRepository.Create(product);
        }

        public async Task Delete(Product product)
        {
            await _ProductRepository.Delete(product);
        }

        public async Task<ResponseBase<ProductDTO>> GetDepartaments()
        {
            return await _ProductRepository.GetDepartaments();
        }

        public async Task<Product> GetProcuctById(int id)
        {
            return await _ProductRepository.GetProcuctById(id);
        }

        public async Task<Product> Update(Product product)
        {
            return await  _ProductRepository.Update(product);
        }
    }
}
