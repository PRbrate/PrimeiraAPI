using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PrimeiraAPI.Model
{
    public class Product : Entity
    {
        public Product()
        {
        }

        public Product(string name, string description, decimal value,int quantity, int quantityInStock, int categoryId)
        {
            Name = name;
            Description = description;
            Value = value;
            Quantity = quantity;
            QuantityInStock = quantityInStock;
            CategoryId = categoryId;
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public int Quantity { get; set; }
        public int QuantityInStock { get; set; }
        public int CategoryId { get; set; }
        public decimal TotalValue => Value * Quantity;

        public void AddStock(int quantity) 
        { 
            QuantityInStock += quantity;
        }
        public void RemoveStock(int quantity)
        {
            if (QuantityInStock > 0)
            {
                if (QuantityInStock - quantity > 0)
                {
                    QuantityInStock -= quantity;
                }
                else
                {
                    throw new Exception($"Não temos a quantidade de {Name} em estoque para a retirada");
                }
                
            }
            else
            {
                throw new Exception($"Não temos a quantidade de {Name} em estoque para a retirada");
            }
        }

    }
}
