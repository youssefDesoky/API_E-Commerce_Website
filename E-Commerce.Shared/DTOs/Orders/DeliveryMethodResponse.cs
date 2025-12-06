using System;

namespace E_Commerce.Shared.DTOs.Orders;

public class DeliveryMethodResponse
{
    public int DeliveryMethodId { get; set; }
    public string ShortName { get; set; }
    public string Description { get; set; }
    public string DeliveryTime { get; set; }
    public decimal Cost { get; set; }
}
