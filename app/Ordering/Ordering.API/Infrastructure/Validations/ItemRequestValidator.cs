namespace Ordering.API.Infrastructure.Validations;

public class ItemRequestValidator : AbstractValidator<ItemRequest>
{
    public ItemRequestValidator()
    {
        RuleFor(request => request.ItemId)
           .NotEmpty().WithMessage("ItemId is required")
           .Must(item => item > 0).WithMessage("Item must be a number and greather than 0");

        RuleFor(request => request.UserId)
            .NotEmpty().WithMessage("UserId is required")
            .Length(1, 100).WithMessage("UserId has to be minimum 1 length and max 100 length");
    }
}
