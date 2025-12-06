using System;
using E_Commerce.Domain.Entities.Orders;

namespace E_Commerce.Service.Specifications;

public class OrderSpecifications : BaseSpecification<Order>
{
    public OrderSpecifications(Guid id, string userEmail) : base(o => o.Id == id && o.UserEmail.ToLower() == userEmail.ToLower())
    {
        Includes.Add(o => o.DeliveryMethod);
        Includes.Add(o => o.OrderItems);
    }

    public OrderSpecifications(string userEmail) : base(o => o.UserEmail.ToLower() == userEmail.ToLower())
    {
        Includes.Add(o => o.DeliveryMethod);
        Includes.Add(o => o.OrderItems);

        AddOrderByDesc(o => o.OrderDate);
    }
}
