using PrimeiraAPI.Model;

namespace PrimeiraAPI.DTOs
{
    public class ProductDTO : Entity
    {

        public ProductDTO()
        {
        }

        public ProductDTO(int id, string description, decimal value, int quantity, int categoryId, string name)
        {
            Id = id;
            Description = description;
            Value = value;
            CategoryID = categoryId;
            Name = name;
        }

        public ProductDTO(int id, string name, string description, decimal value, int quantity, int categoryID, string categoryName, int quantityInSock)
        {
            Id = id;
            Name = name;
            Description = description;
            Value = value;
            Quantity = quantity;
            CategoryID = categoryID;
            CategoryName = categoryName;
            QuantityInStock = quantityInSock;
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public int Quantity { get; set; }
        public decimal TotalValue => Value * Quantity;
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int QuantityInStock { get; set; }
    }
}
