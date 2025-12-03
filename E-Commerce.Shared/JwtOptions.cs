using System;

namespace E_Commerce.Shared;

public class JwtOptions
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SecretKey { get; set; }
    public double DurationInDays { get; set; }
}
