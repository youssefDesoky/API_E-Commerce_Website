using System;

namespace E_Commerce.Shared.ErrorModels;

public class ValidationErrorDetails : ErrorDetails
{
    public IEnumerable<ValidationError> Errors { get; set; }
}