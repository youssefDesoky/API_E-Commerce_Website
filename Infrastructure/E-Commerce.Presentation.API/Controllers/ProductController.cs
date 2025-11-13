using E_Commerce.Service.Abstraction;
using E_Commerce.Shared.DTOs.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService) : APIBaseController
    {
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrandsAsync()
        {
            var brands = await productService.GetBrandsAsync();
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetTypesAsync()
        {
            var types = await productService.GetTypesAsync();
            return Ok(types);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductByIdAsync(int id)
        {
            var product = await productService.GetByIdAsync(id);
            return Ok(product);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsAsync([FromQuery] ProductQueryParameters parameters)
        {
            var products = await productService.GetProductsAsync(parameters);
            return Ok(products);
        }
    }
}
