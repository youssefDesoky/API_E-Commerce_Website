using System;

namespace E_Commerce.Shared.DTOs.Auth;

public class UserResponse
{
    public string DisplayName { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
}
