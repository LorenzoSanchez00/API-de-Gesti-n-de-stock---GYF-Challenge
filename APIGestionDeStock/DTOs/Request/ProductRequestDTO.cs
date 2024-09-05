using APIGestionDeStock.Models.ProductCategory;

namespace APIGestionDeStock.DTOs.Request
{
    public class ProductRequestDTO
    {
        public decimal Price { get; set; }
        public Category Category { get; set; }
    }
}
