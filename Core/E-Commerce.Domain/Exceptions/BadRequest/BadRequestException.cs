using System;

namespace E_Commerce.Domain.Exceptions.BadRequest;

public abstract class BadRequestException(string message) : Exception(message)
{

}
