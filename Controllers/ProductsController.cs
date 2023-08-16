using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimeiraAPI.Data;
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
        public async Task<List<Product>> Get()
        {
            return await _databaseContext.Products.ToListAsync();
        }

        [HttpPost("CreateProducts")]
        public Product Post([FromBody] Product product)
        {
            _databaseContext.Products.Add(product);
            _databaseContext.SaveChangesAsync();
            return product;
        }

        [HttpGet("{id}")]
        public async Task<Product> GetbyId(int id)
        {
            Product produto = await _databaseContext.Products.FindAsync(id);
            return produto;
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
        public Product Update(Product product, int id)
        {
            if (id == product.Id)
            {
                _databaseContext.Products.Update(product);
                _databaseContext.SaveChangesAsync();
            }
            return product;
        }

    }
}
