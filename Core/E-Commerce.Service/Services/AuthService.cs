using System;
using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Domain.Exceptions.BadRequest;
using E_Commerce.Domain.Exceptions.NotFound;
using E_Commerce.Domain.Exceptions.Unauthorized;
using E_Commerce.Service.Abstraction;
using E_Commerce.Shared.DTOs.Auth;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Service.Services;

public class AuthService(UserManager<AppUser> userManager) : IAuthService
{
    public async Task<UserResponse> LoginAsync(LoginRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null) throw new UserNotFoundException(request.Email);
        
        var flag = await userManager.CheckPasswordAsync(user, request.Password);
        if (!flag) throw new UnauthorizedException();

        return new UserResponse
        {
            DisplayName = user.DisplayName,
            Email = user.Email,
            Token = "ToDO",
        };
    }

    public async Task<UserResponse> RegisterAsync(RegisterRequest request)
    {
        var result = await userManager.CreateAsync(new AppUser
        {
            UserName = request.UserName,
            DisplayName = request.DisplayName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
        }, request.Password);

        if (!result.Succeeded) throw new RegistrationBadRequestException(result.Errors.Select(e => e.Description).ToList());

        return new UserResponse
        {
            DisplayName = request.DisplayName,
            Email = request.Email,
            Token = "ToDO",
        };
    }
}
