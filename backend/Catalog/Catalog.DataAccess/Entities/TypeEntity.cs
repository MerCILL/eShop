namespace Catalog.DataAccess.Entities;

public class TypeEntity
{
    public int Id { get; set; }
    public string Title { get; set; } 

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}