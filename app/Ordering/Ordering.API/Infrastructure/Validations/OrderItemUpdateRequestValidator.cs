namespace Ordering.API.Infrastructure.Validations;

public class OrderItemUpdateRequestValidator : AbstractValidator<OrderItemUpdateRequest>
{
    public OrderItemUpdateRequestValidator()
    {
        RuleFor(request => request.ItemId)
            .NotEmpty().WithMessage("ItemId is required")
            .Must(item => item > 0).WithMessage("Item must be a number and greather than 0");

        RuleFor(request => request.Quantity)
            .NotEmpty().WithMessage("Quantity is required")
            .Must(price => price > 0).WithMessage("Quantity must be a number and greater than 0");
    }
}
