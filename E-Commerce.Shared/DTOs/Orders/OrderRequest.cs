using System;

namespace E_Commerce.Shared.DTOs.Orders;

public class OrderRequest
{
    public string BasketId { get; set; }
    public int DeliveryMethodId { get; set; }
    public OrderAddressDto ShipToAddress { get; set; }
}
