using System;

namespace E_Commerce.Domain.Exceptions.NotFound;

public class UserNotFoundException(string email) : NotFoundException($"User with email '{email}' was not found.")
{

}
