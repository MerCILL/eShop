﻿using Basket.API.Infrastructure.Validations;

namespace Basket.API.Infrastructure.Configurations;

public static class ServicesConfiguration
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddHttpClient();

        builder.Services.Configure<ApiClientSettings>
            (builder.Configuration.GetSection("CatalogApiClientSettings"));

        builder.Services.AddScoped<ApiClientHelper>();

        builder.Services.AddScoped<ICatalogService, CatalogService>();
        builder.Services.AddScoped<ICacheService, CacheService>();
        builder.Services.AddScoped<IBasketService, BasketService>();

        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddFluentValidationClientsideAdapters();

        builder.Services.AddLogging(loggingBuilder =>
            loggingBuilder.AddSerilog(dispose: true));

        builder.Services.AddControllers(); 
        builder.Services.AddEndpointsApiExplorer();
    }
}
