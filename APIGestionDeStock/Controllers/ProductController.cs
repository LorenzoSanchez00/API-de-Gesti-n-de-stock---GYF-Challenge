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
            var obj = new APIResponseDTO
            {
                Message = "Products",
                Data = await _productRepository.GetAll()
            };
            try
            {
                return Ok(obj);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        [HttpGet("get-by-id{id:int}")]
        public async Task<IActionResult> GetProductsById(int id)
        {
            var obj = new APIResponseDTO
            {
                Message = "found products",
                Data = await _productRepository.GetById(id)
            };
            try
            {
                return Ok(obj);
            }
            catch (Exception)
            {
                return BadRequest(obj);
            }
        }

        [HttpGet("filter-by-budget")]
        public async Task<IActionResult> FilterByBudget(int budget)
        {
            var obj = new APIResponseDTO
            {
                Message = "filtered products",
                Data = await _productRepository.filterByBudget(budget)
            };
            try
            {
                var validationResult = await _numValidator.ValidateAsync(budget);

                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }

                return Ok(obj);
            }
            catch (Exception)
            {

                return BadRequest(obj);
            }
        }

        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct(ProductRequestDTO productRequestDTO)
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
        public async Task<IActionResult> UpdateProduct(int id, ProductRequestDTO productRequestDTO)
        {
            try
            {
                var newProductModel = productRequestDTO.FromRequestDtoToEntity();
                var product = await _productRepository.Update(id, newProductModel);
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
            var deletedProduct = new APIResponseDTO
            {
                Message = "Deleted Product",
                Data = await _productRepository.Delete(id)
            };
            try
            {
                return Ok(deletedProduct);
            }
            catch (Exception)
            {

                return BadRequest(deletedProduct);
            }
        }

    }
}
