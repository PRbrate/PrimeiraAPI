using Microsoft.EntityFrameworkCore;
using PrimeiraAPI.Data.Repository.Interface;
using PrimeiraAPI.DTOs;
using PrimeiraAPI.Model;

namespace PrimeiraAPI.Data.Repository
{
    public class ProductRepository : IProductRepository
    {

        private readonly DatabaseContext _dbContext;
        public ProductRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Product> Create(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();

            return product;
        }
        public async Task Delete(Product product)
        {
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Product> GetProcuctById(int id)
        {
            Product produto = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            return produto;
        }
        public async Task<ResponseBase<ProductDTO>> GetDepartaments()
        {
            var products = _dbContext.Products.Include(p => p.Category).AsQueryable();
            List<ProductDTO> productsDTOs = new List<ProductDTO>();

            productsDTOs = products.Select(products => new ProductDTO
            (
                products.Id,
                products.Description,
                products.Value,
                products.QuantityInStock,
                products.CategoryId,
                products.Name
            )).ToList();

            var respose = new ResponseBase<ProductDTO>
            {
                Items = productsDTOs,
                TotalItems = productsDTOs.Count
            };

            return await Task.FromResult(respose);
        }
        public async Task<Product> Update(Product product)
        {

            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();

            return product;
        }
    }
}
