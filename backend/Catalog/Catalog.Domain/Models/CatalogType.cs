namespace Catalog.Domain.Models;

public class CatalogType
{
    public int Id { get; }
    public string Title { get; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; }

    public CatalogType(int id, string title, DateTime createdAt, DateTime? updatedAt)
    {
        Id = id;
        Title = title;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public CatalogType(string title, DateTime createdAt, DateTime? updatedAt)
    {
        Title = title;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}
