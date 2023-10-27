using PrimeiraAPI.DTOs;

namespace PrimeiraAPI.Data.Filters
{
    public class ProductFilter
    {
        public ProductDTO ProductDTO {get;set;}
        public int Id { get; set; }
        public int? Quantity { get; set; }
    }
}
