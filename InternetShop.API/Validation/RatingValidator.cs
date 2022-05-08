using FluentValidation;
using InternetShop.BAL.DTOs.Rating;

namespace InternetShop.API.Validation
{
    public class RatingValidator : AbstractValidator<RatingDTO>
    {
        public RatingValidator()
        {
            RuleFor(r=>r.UserId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(r=>r.StarsCount).NotNull().NotEmpty()
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(5);
            RuleFor(r=>r.ProductId).NotNull().NotEmpty().GreaterThan(0);
        }
    }
}
