using System;

namespace E_Commerce.Domain.Exceptions.NotFound;

public class BasketNotFoundException(string id) : NotFoundException($"Basket With Id: {id} Is Not Found")
{

}
