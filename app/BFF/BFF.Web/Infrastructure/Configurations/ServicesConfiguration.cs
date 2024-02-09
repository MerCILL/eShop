namespace BFF.Web.Infrastructure.Configurations;

public static class ServicesConfiguration
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddHttpClient();

        builder.Services.AddScoped<ICatalogBffService, CatalogBffService>();
        builder.Services.AddScoped<IUserBffService, UserBffService>();
        builder.Services.AddScoped<IBasketBffService, BasketBffService>();
        builder.Services.AddScoped<IOrderBffService, OrderBffService>();

        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
    }
}
