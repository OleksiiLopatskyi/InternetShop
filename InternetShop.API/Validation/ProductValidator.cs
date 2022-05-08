using FluentValidation;
using InternetShop.BAL.DTOs.Product;


namespace InternetShop.API.Validation
{
    public class ProductValidator : AbstractValidator<ProductDTO>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty().Length(1, 20);
            RuleFor(p => p.Description).NotEmpty().Length(1, 20);
            RuleFor(p => p.Price).GreaterThan(0).LessThan(999999);
            RuleFor(p => p.QuantityInStock).GreaterThan(0).LessThan(999999);
            RuleFor(p => p.GroupId).GreaterThan(0);
            RuleFor(p => p.Images).NotNull();
            When(p => p.Images != null, () =>
            {
                RuleFor(p => p.Images).Must(p => p.Count > 0 && p.Count <= 5)
                .WithMessage("Product should contain at least 1 or less than 5 images");
            });
        }
    }
}
