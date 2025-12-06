using System;

namespace E_Commerce.Domain.Exceptions.BadRequest;

public class CreateOrderBadRequestException() : BadRequestException("Problem occurred while creating the order.")
{

}
