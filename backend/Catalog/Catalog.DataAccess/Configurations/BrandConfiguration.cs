namespace Catalog.DataAccess.Configurations;

public class BrandConfiguration : IEntityTypeConfiguration<BrandEntity>
{
    public void Configure(EntityTypeBuilder<BrandEntity> builder)
    {
        builder.ToTable("Brand");

        builder.HasKey(brand => brand.Id);

        builder.Property(brand => brand.Title).IsRequired().HasMaxLength(50);
    }
}