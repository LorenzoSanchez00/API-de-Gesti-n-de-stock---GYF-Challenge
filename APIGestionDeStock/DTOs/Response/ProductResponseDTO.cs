using APIGestionDeStock.Models.ProductCategory;

namespace APIGestionDeStock.DTOs.Response
{
    public class ProductResponseDTO
    {
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public Category Category { get; set; }
    }
}
