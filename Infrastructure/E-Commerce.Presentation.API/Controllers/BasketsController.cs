using E_Commerce.Service.Abstraction;
using E_Commerce.Shared.DTOs.Baskets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Presentation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController(IBasketService basketService) : ControllerBase
    {
        [HttpGet("{basketId}")]
        public async Task<IActionResult> GetBasket(string basketId)
        {
            var basket = await basketService.GetBasketAsync(basketId);

            return Ok(basket);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBasket(CustomerBasketDto basket)
        {
            var updatedBasket = await basketService.UpdateBasketAsync(basket, TimeSpan.FromDays(1));

            return Ok(updatedBasket);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasket(string basketId)
        {
            var result = await basketService.DeleteBasketAsync(basketId);

            return NoContent(); // 204 No Content[Success, more used for DELETE and PUT operations]
        }
    }
}
