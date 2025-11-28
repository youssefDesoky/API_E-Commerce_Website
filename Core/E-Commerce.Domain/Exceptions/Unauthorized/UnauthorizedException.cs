using System;

namespace E_Commerce.Domain.Exceptions.Unauthorized;

public class UnauthorizedException() : Exception("You are not authorized")
{

}
