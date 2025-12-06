using System;

namespace E_Commerce.Domain.Exceptions.NotFound;

public class OrderNotFoundException(Guid id) : NotFoundException($"Order With Id: {id} Is Not Found.")
{

}
