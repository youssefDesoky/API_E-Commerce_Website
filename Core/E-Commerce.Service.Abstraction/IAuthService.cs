using System;
using E_Commerce.Shared.DTOs.Auth;

namespace E_Commerce.Service.Abstraction;

public interface IAuthService
{
    Task<UserResponse> LoginAsync(LoginRequest request);
    Task<UserResponse> RegisterAsync(RegisterRequest request);
}
