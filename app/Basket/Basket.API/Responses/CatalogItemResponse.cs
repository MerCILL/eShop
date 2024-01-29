namespace Basket.API.Responses;

public record CatalogItemResponse(
int Id,
string Title,
string Description,
decimal Price,
string PictureFile,
CatalogTypeResponse Type,
CatalogBrandResponse Brand,
int Quantity
);