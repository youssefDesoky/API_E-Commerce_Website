using System;
using System.Text;
using System.Text.Json;
using E_Commerce.Service.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace E_Commerce.Presentation.API.Controllers.Attributes;

public class CacheAttribute(int durationInSeconds) : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
        var cacheKey = GetCacheKey(context.HttpContext.Request);

        var result = await cacheService.GetAsync(cacheKey);

        if (!string.IsNullOrEmpty(result))
        {
            var response = new ContentResult
            {
                Content = result,
                ContentType = "application/json",
                StatusCode = 200
            };

            context.Result = response;
            return;
        }

        var actionContext = await next.Invoke();
        if (actionContext.Result is OkObjectResult okObjectResult)
        {
            await cacheService.SetAsync(cacheKey, okObjectResult.Value, TimeSpan.FromSeconds(durationInSeconds));
        }
    }

    private string GetCacheKey(HttpRequest request)
    {
        var keyBuilder = new StringBuilder();

        keyBuilder.Append($"{request.Path}");

        foreach (var item in request.Query)
        {
            keyBuilder.Append($"|{item.Key}-{item.Value}");
        }

        return keyBuilder.ToString();
    }
}
