using System;
using E_Commerce.Domain.Entities.Orders.Enums;

namespace E_Commerce.Domain.Entities.Orders;

public class Order : Entity<Guid>
{
    public Order() { }
    
    public Order(string userEmail, ShippingAddress shippingAddress, DeliveryMethod deliveryMethod, decimal subtotal, ICollection<OrderItem> orderItems)
    {
        UserEmail = userEmail;
        ShippingAddress = shippingAddress;
        DeliveryMethod = deliveryMethod;
        Subtotal = subtotal;
        OrderItems = orderItems;
    }

    public string UserEmail { get; set; }
    public DateTimeOffset OrderDate { get; set; } = DateTime.Now;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public ShippingAddress ShippingAddress { get; set; }

    public int DeliveryMethodId { get; set; }
    public DeliveryMethod DeliveryMethod { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public decimal Subtotal { get; set; }

    public decimal GetTotal() => Subtotal + DeliveryMethod.Cost; // Will Not be mapped
}