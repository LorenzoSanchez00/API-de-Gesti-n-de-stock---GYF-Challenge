using APIGestionDeStock.DTOs.Request;
using APIGestionDeStock.DTOs.Response;
using APIGestionDeStock.Mappers;
using APIGestionDeStock.Models;
using APIGestionDeStock.Repositories.Classes;
using APIGestionDeStock.Repository.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace APIGestionDeStock.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IValidator<ProductRequestDTO> _Addvalidator;
        private readonly IValidator<int> _numValidator;
        public ProductController(IProductRepository productRepository,
            IValidator<ProductRequestDTO> addValidator,
            IValidator<int> numValidator)
        {
            _productRepository = productRepository;
            _Addvalidator = addValidator;
            _numValidator = numValidator;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var products = await _productRepository.GetAll();
                if (products == null) return NotFound("No results found");
                return Ok(products);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("get-by-id{id:int}")]
        public async Task<IActionResult> GetProductsById(int id)
        {
            try
            {
                var products = await _productRepository.GetById(id);
                if (products == null) return NotFound("No results found");
                return Ok(products);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("filter-by-budget")]
        public async Task<IActionResult> FilterByBudget(int budget)
        {
            try
            {
                var validationResult = await _numValidator.ValidateAsync(budget);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }

                var products = await _productRepository.filterByBudget(budget);
                if (!products.Any()) return NotFound("No products were found that met these conditions");
                return Ok(products);
            }
            catch (Exception)
            {
                return BadRequest();           
            }
        }

        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct([FromBody] ProductRequestDTO productRequestDTO)
        {
            try
            {
                var validationResult = await _Addvalidator.ValidateAsync(productRequestDTO);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }

                var newProductModel = productRequestDTO.FromRequestDtoToEntity();
                var product = await _productRepository.Add(newProductModel);
                return Ok(product.FromEntityToResponseDto());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("update{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductRequestDTO productRequestDTO)
        {
            try
            {
                var newProductModel = productRequestDTO.FromRequestDtoToEntity();
                var product = await _productRepository.Update(id, newProductModel);
                if (product == null) return NotFound("No results found");
                return Ok(product.FromEntityToResponseDto());
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpDelete("delete{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var deletedProduct = await _productRepository.Delete(id);
                if (deletedProduct == null) return NotFound("No results found");
                return Ok(deletedProduct);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}
