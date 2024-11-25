using Catalog.API.DTO;
using FluentValidation;

namespace Catalog.API.Validators;

public class RestaurantRequestValidator : AbstractValidator<RestaurantRequest>
{
    public RestaurantRequestValidator()
    {
        RuleFor(r => r.Name).NotEmpty()
            .WithMessage("Name is required");
        RuleFor(r => r.Name).Length(3, 100)
            .WithMessage("Name must be between 3 and 100 characters");
        RuleFor(r => r.AddressId).NotEqual(Guid.Empty)
            .WithMessage("Address is invalid");
    }
}