using FluentValidation;
using InternetShop.BAL.DTOs.Order;

namespace InternetShop.API.Validation.Order
{
    public class OrderReceiverValidator : AbstractValidator<OrderReceiverDTO>
    {
        public OrderReceiverValidator()
        {
            RuleFor(r=>r.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(r=>r.Name).NotNull().NotEmpty();
        }
    }
}
