namespace WebApp.Infrastructure.Configurations;

public static class ServicesConfiguration
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddControllersWithViews();
        builder.Services.AddSession();
        builder.Services.AddHttpClient();

        builder.Services.AddScoped<ICatalogService, CatalogService>();
        builder.Services.AddScoped<IBasketService, BasketService>();
        builder.Services.AddScoped<IOrderService, OrderService>();
        builder.Services.AddScoped<ILoginService, LoginService>();
    }
}
