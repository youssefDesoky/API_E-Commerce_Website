using System;

namespace E_Commerce.Domain.Exceptions.NotFound;

public class DeliveryMethodNotFound(int id) : NotFoundException($"Delivery Method With Id: {id} Is Not Found.")
{

}
