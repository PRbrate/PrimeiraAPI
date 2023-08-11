namespace PrimeiraAPI.Model
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public int Quantity { get; set; }
        public decimal TotalValue => Value * Quantity;

    }
}
