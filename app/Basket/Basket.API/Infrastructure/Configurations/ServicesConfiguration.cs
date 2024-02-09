namespace Basket.API.Infrastructure.Configurations;

public static class ServicesConfiguration
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddHttpClient();

        builder.Services.AddScoped<ICatalogService, CatalogService>();
        builder.Services.AddScoped<ICacheService, CacheService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IBasketService, BasketService>();

        builder.Services.AddControllers();       
        builder.Services.AddEndpointsApiExplorer();


    }
}
