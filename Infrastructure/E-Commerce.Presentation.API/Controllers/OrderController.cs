using System.Security.Claims;
using System.Threading.Tasks;
using E_Commerce.Service.Abstraction;
using E_Commerce.Shared.DTOs.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Presentation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController(IOrderService orderService) : ControllerBase
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrder(OrderRequest orderRequest)
        {
            var userEmailClaim = User.FindFirst(ClaimTypes.Email);
            var result = await orderService.CreateOrderAsync(orderRequest, userEmailClaim.Value);
            return Ok(result);
        }

        [HttpGet("deliveryMethods")]
        public async Task<IActionResult> GetAllDeliveryMethods()
        {
            var result = await orderService.GetAllDeliveryMethodsAsync();
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserOrders()
        {
            var userEmailClaim = User.FindFirst(ClaimTypes.Email);
            var result = await orderService.GetAllOrdersForUserAsync(userEmailClaim.Value);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserOrderById(Guid id)
        {
            var userEmailClaim = User.FindFirst(ClaimTypes.Email);
            var result = await orderService.GetOrderByIdForUserAsync(id, userEmailClaim.Value);
            return Ok(result);
        }
    }
}
