using System;

namespace E_Commerce.Shared.DTOs.Orders;

public class OrderResponse
{
    public Guid OrderId { get; set;}
    public string UserEmail { get; set; }
    public DateTimeOffset OrderDate { get; set; } = DateTime.Now;
    public OrderAddressDto ShippingAddress { get; set; }

    public string DeliveryMethod { get; set; }

    public ICollection<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();

    public decimal Subtotal { get; set; }

    public decimal Total { get; set; }
}
