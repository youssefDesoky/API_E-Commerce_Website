using System;
using E_Commerce.Shared.DTOs.Orders;

namespace E_Commerce.Service.Abstraction;

public interface IOrderService
{
    Task<OrderResponse?> CreateOrderAsync(OrderRequest orderRequest, string userEmail);
    Task<IEnumerable<DeliveryMethodResponse>> GetAllDeliveryMethodsAsync();
    Task<OrderResponse?> GetOrderByIdForUserAsync(Guid orderId, string userEmail);
    Task<IEnumerable<OrderResponse>?> GetAllOrdersForUserAsync(string userEmail);
}
