namespace Catalog.DataAccess.Entities;

public class ItemEntity
{
    public int Id { get; set; }
    public string Title { get; set; } 
    public string Description { get; set; } 
    public decimal Price { get; set; }
    public string PictureFile { get; set; }

    public int TypeId { get; set; }
    public TypeEntity Type { get; set; }

    public int BrandId { get; set; }
    public BrandEntity Brand { get; set; } 

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

}
