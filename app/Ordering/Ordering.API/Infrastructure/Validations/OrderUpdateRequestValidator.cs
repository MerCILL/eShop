namespace Ordering.API.Infrastructure.Validations;

public class OrderUpdateRequestValidator : AbstractValidator<OrderUpdateRequest>
{
    public OrderUpdateRequestValidator()
    {
        RuleFor(request => request.Address)
           .NotEmpty().WithMessage("Address is required")
           .Length(1, 300).WithMessage("Address has to be minimum 1 length and max 300 length")
           .Matches("^[a-zA-Z0-9 ]*$").WithMessage("Address can only contain alphanumeric characters and spaces");

        RuleFor(request => request.Items)
          .Must(items => items.Count > 0).WithMessage("Number of items has to be > 0");

        RuleForEach(request => request.Items)
            .SetValidator(new OrderItemUpdateRequestValidator())
            .WithMessage("Error in OrderItemUpdateRequest");
    }
}
