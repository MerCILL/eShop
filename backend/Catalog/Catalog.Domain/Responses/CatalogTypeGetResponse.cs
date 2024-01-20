namespace Catalog.Domain.Responses;

public record CatalogTypeGetResponse(
    int Id,
    string Title,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
