using System;

namespace E_Commerce.Domain.Exceptions.BadRequest;

public class BasketBadRequestException(string id) : BadRequestException("Basket Bad Request Exception Occurred")
{
    
}
