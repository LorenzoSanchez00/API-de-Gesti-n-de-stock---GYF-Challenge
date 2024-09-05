using APIGestionDeStock.Models.ProductCategory;

namespace APIGestionDeStock.Models
{
    public class Product : BaseModel
    {
        public decimal Price { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public Category Category { get; set; }
    }
}