using System;

namespace E_Commerce.Domain.Exceptions.BadRequest;

public class UpdateAndCreateBadRequestException(string id) : BadRequestException("Update And Create Bad Request Exception Occurred")
{

}
