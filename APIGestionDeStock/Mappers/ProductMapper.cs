using APIGestionDeStock.DTOs.Request;
using APIGestionDeStock.DTOs.Response;
using APIGestionDeStock.Models;

namespace APIGestionDeStock.Mappers
{
    public static class ProductMapper
    {
        public static Product FromRequestDtoToEntity(this ProductRequestDTO productRequestDTO)
        {
            return new Product
            {
                Price = productRequestDTO.Price,
                Date = DateTime.Now,
                Category = productRequestDTO.Category
            };
        }

        public static ProductRequestDTO FromEntityToRequestDto(this Product product)
        {
            return new ProductRequestDTO
            {
                Price = product.Price,
                Category = product.Category
            };
        }

        public static ProductResponseDTO FromEntityToResponseDto(this Product product)
        {
            return new ProductResponseDTO
            {
                Price = product.Price,
                Date = product.Date,
                Category = product.Category
            };
        }
    }
}
