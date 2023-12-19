namespace Jwt.Api.Validations.ProductValidations;

public class CreateProductValidator:AbstractValidator<AddProductDto>
{
    public CreateProductValidator()
    {
        RuleFor(p=>p.ProductName).NotEmpty().NotNull().Length(3,30);
        RuleFor(p => p.Price).GreaterThanOrEqualTo(0);
        RuleFor(p=>p.Quantity).GreaterThanOrEqualTo(0);
    }
}

