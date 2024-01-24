var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("CatalogDb");
builder.Services.AddDbContext<CatalogDbContext>(options => options.UseNpgsql(connectionString, b => b.MigrationsAssembly("Catalog.API")));


builder.Services.AddScoped<ICatalogTypeRepository<CatalogTypeEntity>, CatalogTypeRepository>();
builder.Services.AddScoped<ICatalogTypeService, CatalogTypeService>();

builder.Services.AddScoped<ICatalogBrandRepository<CatalogBrandEntity>, CatalogBrandRepository>();
builder.Services.AddScoped<ICatalogBrandService, CatalogBrandService>();

builder.Services.AddScoped<ICatalogItemRepository<CatalogItemEntity>, CatalogItemRepository>();
builder.Services.AddScoped<ICatalogItemService, CatalogItemService>();


builder.Services.AddAutoMapper(
    typeof(EntityToModelMapperProfile),
    typeof(ModelToResponseMapperProfile),
    typeof(RequestToModelMapperProfile));

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<CatalogTypeRequestValidator>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

CatalogDbInitializer.EnsureDatabaseCreated(app.Services);

app.Run();


