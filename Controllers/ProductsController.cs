using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimeiraAPI.Data;
using PrimeiraAPI.Data.Repository;
using PrimeiraAPI.DTOs;
using PrimeiraAPI.Model;

namespace PrimeiraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductRepository _productRepository;
        public ProductsController(ProductRepository productRepository)
        {
            _productRepository = productRepository;

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _productRepository.GetDepartaments();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ProductDTO> GetProductDTO(int id)
        {
            Product produto = await _productRepository.GetProcuctById(id);
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
        public async Task<ProductDTO> Post([FromBody] ProductDTO productDTO)
        {
            Product product = new Product()
            {
                Name = productDTO.Name,
                Description = productDTO.Description,
                Value = productDTO.Value,
                Quantity = productDTO.Quantity
            };
            
            await _productRepository.Create(product);
            return productDTO;
        }

        [HttpPut("{id}")]
        public async Task<ProductDTO> Update(ProductDTO productDTO, int id)
        {
            Product product = await _productRepository.GetProcuctById(id);

            if (id == product.Id)
            {
                await _productRepository.Update(product);
            }
            return productDTO;
        }


        [HttpDelete("{id}")]
        public async Task<Product> Delete(int id)
        {
            Product produto = await _productRepository.GetProcuctById(id);
            await _productRepository.Delete(produto);
            return produto;
        }

    }
}
