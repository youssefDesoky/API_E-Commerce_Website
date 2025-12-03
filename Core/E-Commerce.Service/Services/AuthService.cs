using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Domain.Exceptions.BadRequest;
using E_Commerce.Domain.Exceptions.NotFound;
using E_Commerce.Domain.Exceptions.Unauthorized;
using E_Commerce.Service.Abstraction;
using E_Commerce.Shared;
using E_Commerce.Shared.DTOs.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace E_Commerce.Service.Services;

public class AuthService(UserManager<AppUser> userManager, IOptions<JwtOptions> jwtOptions) : IAuthService
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
            Token = await GenerateJwtToken(user),
        };
    }

    public async Task<UserResponse> RegisterAsync(RegisterRequest request)
    {
        var user = new AppUser()
        {
            UserName = request.UserName,
            DisplayName = request.DisplayName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber
        };
        
        var result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded) throw new RegistrationBadRequestException(result.Errors.Select(e => e.Description).ToList());

        return new UserResponse
        {
            DisplayName = request.DisplayName,
            Email = request.Email,
            Token = await GenerateJwtToken(user)
        };
    }

    private async Task<string> GenerateJwtToken(AppUser user)
    {
        var jwtOpts = jwtOptions.Value;

        var claims = new List<Claim>(
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.DisplayName),
            new Claim(ClaimTypes.MobilePhone, user.PhoneNumber)
        );

        var uesrRoles = userManager.GetRolesAsync(user).Result;
        foreach (var role in uesrRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtOpts.SecretKey));

        var token = new JwtSecurityToken(
            issuer: jwtOpts.Issuer,
            audience: jwtOpts.Audience,
            expires: DateTime.UtcNow.AddDays(jwtOpts.DurationInDays),
            claims: claims,
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
