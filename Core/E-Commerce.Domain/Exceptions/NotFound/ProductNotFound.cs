using System;

namespace E_Commerce.Domain.Exceptions.NotFound;

public class ProductNotFound(int id) : NotFoundException($"Product With Id: {id} Is Not Found.")
{

}
