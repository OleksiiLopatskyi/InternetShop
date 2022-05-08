using FluentValidation;
using InternetShop.BAL.DTOs.User;

namespace InternetShop.API.Validation
{
    public class UserLoginValidator : AbstractValidator<LoginUserDTO>
    {
        public UserLoginValidator()
        {
            RuleFor(r=>r.Email).NotNull().NotEmpty();
            RuleFor(r=>r.Password).NotNull().NotEmpty();
        }
    }
}
