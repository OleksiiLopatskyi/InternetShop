using FluentValidation;
using InternetShop.BAL.DTOs.Brand;

namespace InternetShop.API.Validation
{
    public class BrandValidator : AbstractValidator<BrandDTO>
    {
        public BrandValidator()
        {
            RuleFor(g=>g.Name).NotNull().NotEmpty().Length(1,20);
        }
    }
}
