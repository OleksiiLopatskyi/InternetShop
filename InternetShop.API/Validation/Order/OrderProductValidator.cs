using FluentValidation;
using InternetShop.BAL.DTOs.Order;

namespace InternetShop.API.Validation.Order
{
    public class OrderProductValidator : AbstractValidator<OrderProductDTO>
    {
        public OrderProductValidator()
        {
            RuleFor(p => p.ProductId).NotNull().GreaterThan(0);
            RuleFor(p=>p.Count).NotNull().GreaterThan(0);
        }
    }
}
