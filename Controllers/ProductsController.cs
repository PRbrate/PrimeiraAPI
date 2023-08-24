using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimeiraAPI.Data;
using PrimeiraAPI.DTOs;
using PrimeiraAPI.Model;

namespace PrimeiraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;
        public ProductsController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;

        }

        [HttpGet]
        public async Task<List<ProductDTO>> Get()
        {
            List<Product> products = await _databaseContext.Products.Include(p => p.Category).ToListAsync();
            List<ProductDTO> productsDTOs = new List<ProductDTO>();

            foreach (var product in products)
            {
                var category = _databaseContext.Products.FirstOrDefault(c => c.Id == product.CategoryId);
                var productsDTO = new ProductDTO(
                    product.Id,
                    product.Description,
                    product.Value,
                    product.Quantity,
                    product.CategoryId,
                    category.Name
                    );
                productsDTOs.Add( productsDTO );
            }

            return productsDTOs;
        }

        [HttpGet("{id}")]
        public ProductDTO GetProductDTO(int id)
        {
            Product produto = new Product();
            produto = _databaseContext.Products.FirstOrDefault(p => p.Id == id);
            ProductDTO productDTO = new ProductDTO()
            {
                Id = produto.Id,
                Name = produto.Name,
                Description = produto.Description,
                Value = produto.Value,
                Quantity = produto.Quantity,
            };
            return productDTO;
        }

        [HttpPost("CreateProducts")]
        public ProductDTO Post([FromBody] ProductDTO productDTO)
        {
            Product product = new Product()
            {
                Name = productDTO.Name,
                Description = productDTO.Description,
                Value = productDTO.Value,
                Quantity = productDTO.Quantity
            };

            _databaseContext.Products.Add(product);
            _databaseContext.SaveChangesAsync();
            return productDTO;
        }


        [HttpDelete("{id}")]
        public async Task<Product> Delete(int id)
        {
            Product produto = await _databaseContext.Products.FindAsync(id);
            _databaseContext.Products.Remove(produto);
            await _databaseContext.SaveChangesAsync();
            return produto;
        }

        [HttpPut("{id}")]
        public ProductDTO Update(ProductDTO productDTO, int id)
        {
            Product product = _databaseContext.Products.FirstOrDefault(p => p.Id == id);

            if (id == product.Id)
            {
                _databaseContext.Products.Update(product);
                _databaseContext.SaveChangesAsync();
            }
            return productDTO;
        }

    }
}
