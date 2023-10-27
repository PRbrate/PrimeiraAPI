using Microsoft.AspNetCore.Mvc;
using PrimeiraAPI.Data.Filters;
using PrimeiraAPI.DTOs;
using PrimeiraAPI.Model;
using PrimeiraAPI.Service.Interface;

namespace PrimeiraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productService;

        public ProductsController(IProductsService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _productService.GetDepartaments();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ProductDTO> GetProductDTO(int id)
        {
            Product produto = await _productService.GetProcuctById(id);
            ProductDTO productDTO = new ProductDTO()
            {
                Id = produto.Id,
                Name = produto.Name,
                Description = produto.Description,
                Value = produto.Value,
                QuantityInStock = produto.QuantityInStock,
            };
            return productDTO;
        }

        [HttpPost("CreateProducts")]
        public async Task<ProductDTO> Post(ProductDTO productDTO)
        {
            Product product = new Product()
            {
                Name = productDTO.Name,
                Description = productDTO.Description,
                Value = productDTO.Value,
                CategoryId = productDTO.CategoryID,
                QuantityInStock = productDTO.QuantityInStock
            };

            await _productService.Create(product);
            return productDTO;
        }

        [HttpPut("{id}")]
        public async Task<ProductDTO> Update([FromBody] ProductDTO productDTO, int id)
        {
            Product product = await _productService.GetProcuctById(id);

            if (id == product.Id)
            {
                await _productService.Update(product);
            }
            return productDTO;
        }

        [HttpPut("v2 {id}")]
        public async Task<Product> AddAndRemove(int id, int? Addquantity, int? RemoveQuantity)
        {

            Product product = await _productService.GetProcuctById(id);

            if (id == product.Id)
            {
                if (Addquantity != null)
                {
                    int q = (int)Addquantity;
                    product.AddStock(q);
                }
                if (RemoveQuantity != null)
                {
                    int q = (int)RemoveQuantity;
                    product.RemoveStock(q);
                }
                await _productService.Update(product);
            }
            return product;
        }

        [HttpDelete("{id}")]
        public async Task<Product> Delete(int id)
        {
            Product produto = await _productService.GetProcuctById(id);
            await _productService.Delete(produto);
            return produto;
        }

    }
}
