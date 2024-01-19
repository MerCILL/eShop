namespace Catalog.DataAccess.Entities;

public class BrandEntity
{
    public int Id { get; set; }
    public string Title { get; set; } 

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}