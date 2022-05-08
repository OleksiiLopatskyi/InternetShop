using FluentValidation;
using InternetShop.DAL.QueryParams;

namespace InternetShop.API.Validation
{
    public class PaginationValidator : AbstractValidator<PaginationParameters>
    {
        public PaginationValidator()
        {
            RuleFor(p=>p.PageNumber).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(p=>p.PageSize).NotNull().NotEmpty().GreaterThan(0).LessThan(50);
        }
    }
}
