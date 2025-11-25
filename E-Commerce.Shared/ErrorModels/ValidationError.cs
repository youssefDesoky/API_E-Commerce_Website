using System;

namespace E_Commerce.Shared.ErrorModels;

public class ValidationError
{
    public string Field { get; set; }
    public IEnumerable<string> Messages { get; set; }
}