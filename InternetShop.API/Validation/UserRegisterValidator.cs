using FluentValidation;
using InternetShop.BAL.DTOs.User;

namespace InternetShop.API.Validation
{
    public class UserRegisterValidator : AbstractValidator<RegisterUserDTO>
    {
        public UserRegisterValidator()
        {
            RuleFor(u => u.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(u => u.FirstName).NotNull().NotEmpty().Length(2, 50);
            RuleFor(u => u.LastName).NotNull().NotEmpty().Length(2, 50);    
            RuleFor(u => u.Password).NotNull().NotEmpty().Length(6, 50);
            RuleFor(u=>u.ConfirmPassword).NotNull().NotEmpty().Equal(u=>u.Password);
        }
    }
}
