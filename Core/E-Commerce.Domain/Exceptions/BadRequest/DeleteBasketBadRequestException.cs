using System;

namespace E_Commerce.Domain.Exceptions.BadRequest;

public class DeleteBasketBadRequestException(string id) : BadRequestException("Delete Basket Bad Request Exception Occurred")
{

}
