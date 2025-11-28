using System;

namespace E_Commerce.Domain.Exceptions.BadRequest;

public class RegistrationBadRequestException(List<string> errors) : BadRequestException(string.Join(", ", errors))
{

}
