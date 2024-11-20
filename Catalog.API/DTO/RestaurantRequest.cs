using FluentValidation;

namespace Catalog.API.DTO;

public class RestaurantRequest
{
    public string Name { get; set; }

    public RestaurantRequest(string name)
    {
        Name = name;
    }
}

public class RestaurantRequestValidator : AbstractValidator<RestaurantRequest>
{
    public RestaurantRequestValidator()
    {
        RuleFor(r => r.Name).NotEmpty()
            .WithMessage("Name is required");
        RuleFor(r => r.Name).Length(3, 100)
            .WithMessage("Name must be between 3 and 100 characters");
    }
}