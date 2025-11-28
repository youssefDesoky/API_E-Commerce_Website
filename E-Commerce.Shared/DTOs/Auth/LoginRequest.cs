using System;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Shared.DTOs.Auth;

public class LoginRequest
{
    [EmailAddress]
    public string Email { get; set; }
    public string Password { get; set;}
}
