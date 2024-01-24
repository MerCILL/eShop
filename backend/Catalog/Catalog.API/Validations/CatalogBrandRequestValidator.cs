namespace Catalog.API.Validations;

public class CatalogBrandRequestValidator : AbstractValidator<CatalogBrandRequest>
{
    private readonly ICatalogBrandService _catalogBrandService;

    public CatalogBrandRequestValidator(ICatalogBrandService catalogBrandService)
    {
        _catalogBrandService = catalogBrandService;

        RuleFor(brand => brand.Title)
            .NotEmpty().WithMessage("Title is required")
            .Length(3, 50).WithMessage("Title has to be length between 3 and 50 characters")
            .Matches("^[a-zA-Z0-9 ]*$").WithMessage("Title can only contain alphanumeric characters and spaces");
    }
}
