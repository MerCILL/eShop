namespace Catalog.DataAccess;

public class CatalogDbContext : DbContext
{
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
    {

    }

    public DbSet<TypeEntity> Types { get; set; }
    public DbSet<BrandEntity> Brands { get; set; }
    public DbSet<ItemEntity> Items { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new TypeConfiguration());
        builder.ApplyConfiguration(new BrandConfiguration());
        builder.ApplyConfiguration(new ItemConfiguration());
    }
}
