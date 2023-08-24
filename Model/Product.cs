namespace PrimeiraAPI.Model
{
    public class Product : Entity
    {
        public Product()
        {
        }

        public Product(string name, string description, decimal value, int quantity, int categoryId) 
        { 
            Name = name;
            Description = description;
            Value = value;
            Quantity = quantity;
            CategoryId = categoryId;
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public int Quantity { get; set; }
        public Category Category { get; set; }

        public int CategoryId { get; set; }
        public decimal TotalValue => Value * Quantity;

    }
}
