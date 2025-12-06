using System;
using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Domain.Exceptions.BadRequest;
using E_Commerce.Domain.Exceptions.NotFound;
using E_Commerce.Service.Abstraction;
using E_Commerce.Service.Specifications;
using E_Commerce.Shared.DTOs.Orders;

namespace E_Commerce.Service.Services;

public class OrderService(IUnitOfWork unitOfWork, IMapper mapper, IBasketRepository basketRepository) : IOrderService
{
    public async Task<OrderResponse?> CreateOrderAsync(OrderRequest orderRequest, string userEmail)
    {
        // 1. Get Order Address
        var orderAddress = mapper.Map<ShippingAddress>(orderRequest.ShipToAddress);

        // 2. Get Delivery Method by Id
        var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderRequest.DeliveryMethodId);
        if (deliveryMethod is null) throw new DeliveryMethodNotFound(orderRequest.DeliveryMethodId);

        // 3. Get Order Items
        // 3.1 Get Basket by Id
        var basket = await basketRepository.GetBasketAsync(orderRequest.BasketId);
        if (basket is null) throw new BasketNotFoundException(orderRequest.BasketId);

        // 3.2 Convert Basket Items to Order Items
        var OrderItems = new List<OrderItem>();

        foreach (var item in basket.Items)
        {
            var product = await unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id);
            if (product is null) throw new ProductNotFound(item.Id);

            // Handle price Mismatch
            if (product.Price != item.Price)
            {
                item.Price = product.Price;
            }

            var ProductItemOrdered = new ProductItemOrdered(item.Id, item.ProductName, item.PictureUrl);
            var orderItem = new OrderItem(ProductItemOrdered, item.Price, item.Quantity);
            OrderItems.Add(orderItem);
        }

        // 4. Calculate Subtotal
        var subtotal = OrderItems.Sum(oi => oi.Price * oi.Quantity);

        // 5. Create Order
        var order = new Order(userEmail, orderAddress, deliveryMethod, subtotal, OrderItems);

        // 6. Save to Database
        unitOfWork.GetRepository<Order, Guid>().Add(order);
        var result = await unitOfWork.SaveChangesAsync();
        if (result <= 0) throw new CreateOrderBadRequestException();

        return mapper.Map<OrderResponse>(order);
    }

    public async Task<IEnumerable<DeliveryMethodResponse>> GetAllDeliveryMethodsAsync()
    {
        var deliveryMethods = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
        return mapper.Map<ICollection<DeliveryMethodResponse>>(deliveryMethods);
    }

    public async Task<OrderResponse?> GetOrderByIdForUserAsync(Guid orderId, string userEmail)
    {
        var specs = new OrderSpecifications(orderId, userEmail);
        
        var order = await unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(specs);
        if (order is null) throw new OrderNotFoundException(orderId);

        return mapper.Map<OrderResponse>(order);
    }

    public async Task<IEnumerable<OrderResponse>?> GetAllOrdersForUserAsync(string userEmail)
    {
        var specs = new OrderSpecifications(userEmail);
        
        var order = await unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(specs);

        return mapper.Map<IEnumerable<OrderResponse>>(order);
    }
}
