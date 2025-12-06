using System;

namespace E_Commerce.Domain.Exceptions.NotFound;

public class UserNotFoundException(string email) : NotFoundException($"User With Email: '{email}' Is Not Found.")
{

}
