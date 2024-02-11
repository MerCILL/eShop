namespace Ordering.API.Infrastructure.Validations;

public class OrderRequestValidator : AbstractValidator<OrderRequest>
{
    public OrderRequestValidator()
    {
        RuleFor(request => request.UserId)
            .NotEmpty().WithMessage("UserId is required")
            .Length(1, 100).WithMessage("UserId has to be minimum 1 length and max 100 length");

        RuleFor(request => request.Address)
            .NotEmpty().WithMessage("Address is required")
            .Length(1, 300).WithMessage("Address has to be minimum 1 length and max 300 length")
            .Matches("^[a-zA-Z0-9 ]*$").WithMessage("Title can only contain alphanumeric characters and spaces");
    }
}
