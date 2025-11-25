using E_Commerce.Service.Abstraction;
using E_Commerce.Shared;
using E_Commerce.Shared.DTOs.Products;
using E_Commerce.Shared.ErrorModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService) : APIBaseController
    {
        [HttpGet("brands")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BrandDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorDetails))]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrandsAsync()
        {
            var brands = await productService.GetBrandsAsync();
            return Ok(brands);
        }


        [HttpGet("types")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TypeDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorDetails))]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetTypesAsync()
        {
            var types = await productService.GetTypesAsync();
            return Ok(types);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<ProductDto>> GetProductByIdAsync(int id)
        {
            var product = await productService.GetByIdAsync(id);
            return Ok(product);
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResult<ProductDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsAsync([FromQuery] ProductQueryParameters parameters)
        {
            var products = await productService.GetProductsAsync(parameters);
            return Ok(products);
        }
    }
}
