namespace Catalog.DataAccess.Configurations;

public class TypeConfiguration : IEntityTypeConfiguration<TypeEntity>
{
    public void Configure(EntityTypeBuilder<TypeEntity> builder)
    {
        builder.ToTable("Type");

        builder.HasKey(type => type.Id);

        builder.Property(type => type.Title).IsRequired().HasMaxLength(50);
    }
}
