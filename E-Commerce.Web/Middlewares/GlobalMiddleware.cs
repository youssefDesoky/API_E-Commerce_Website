using E_Commerce.Domain.Exceptions.BadRequest;
using E_Commerce.Domain.Exceptions.NotFound;
using E_Commerce.Domain.Exceptions.Unauthorized;
using E_Commerce.Shared.ErrorModels;
using Microsoft.AspNetCore.Http.HttpResults;

namespace E_Commerce.Web.Middlewares;

public class GlobalMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next.Invoke(context);
            if (context.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                context.Response.ContentType = "application/json";

                var response = new ErrorDetails
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"The Endpoint Of Path: {context.Request.Path} Is Not Found"
                };

                await context.Response.WriteAsJsonAsync(response);    
            }
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                BadRequestException => StatusCodes.Status400BadRequest,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                _ => StatusCodes.Status500InternalServerError
            };

            var response = new ErrorDetails
            {
                StatusCode = context.Response.StatusCode,
                Message = ex.Message
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
