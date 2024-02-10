namespace Ordering.API.Infrastructure.Configurations;

public static class ServicesConfiguration
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddHttpClient();

        builder.Services.AddScoped<ApiClientHelper>();

        builder.Services.Configure<CatalogApiClientSettings>
            (builder.Configuration.GetSection("CatalogApiClientSettings"));

        builder.Services.Configure<BasketApiClientSettings>
          (builder.Configuration.GetSection("BasketApiClientSettings"));

        builder.Services.AddScoped<IUserRepository<UserEntity>, UserRepository>();
        builder.Services.AddScoped<IUserService, UserService>();

        builder.Services.AddScoped<IOrderRepository<OrderEntity>, OrderRepository>();
        builder.Services.AddScoped<IOrderService, OrderService>();

        builder.Services.AddScoped<ITransactionService, TransactionService>();

        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
    }
}
