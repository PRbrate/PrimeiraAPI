using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimeiraAPI.Data;
using PrimeiraAPI.Model;

namespace PrimeiraAPI.Controllers
{
    public class ProductsController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;
        public ProductsController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;

        }

        [HttpGet("GetProducts")]
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



    }
}
