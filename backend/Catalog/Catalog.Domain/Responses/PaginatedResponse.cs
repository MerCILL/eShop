namespace Catalog.Domain.Responses;

public record PaginatedResponse<TModel>(
    int page,
    int perPage,
    int total,
    int totalPages,
    IEnumerable<TModel> Data
);
