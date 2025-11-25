using E_Commerce.Domain.Contracts;
using E_Commerce.Persistence.Context;
using E_Commerce.Persistence.DbInitializer;
using E_Commerce.Persistence.Repositories;
using E_Commerce.Service.Abstraction;
using E_Commerce.Service.MappingProfile;
using E_Commerce.Service.Services;
using E_Commerce.Shared.ErrorModels;
using E_Commerce.Web.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Configure DbContext
builder.Services.AddDbContext<StoreDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
});
#endregion

#region Injections
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddAutoMapper(x => x.AddProfile(new ProductProfile(builder.Configuration)));
builder.Services.AddAutoMapper(x => x.AddProfile(new BasketProfile()));
#endregion

#region Validation Error Exception
builder.Services.Configure<ApiBehaviorOptions>(config =>
{
    config.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
                    .Where(e => e.Value.Errors.Any())
                    .Select(e => new ValidationError()
                    {
                        Field = e.Key,
                        Messages = e.Value.Errors.Select(er => er.ErrorMessage)
                    }).ToList();

        var errorDetails = new ValidationErrorDetails
        {
            StatusCode = StatusCodes.Status400BadRequest,
            Message = "One Or More Validation Errors Occurred.",
            Errors = errors
        };

        return new BadRequestObjectResult(errorDetails);
    };
});
#endregion

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    await dbInitializer.InitializeAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<GlobalMiddleware>();

app.UseStaticFiles();

app.MapControllers();

app.Run();
