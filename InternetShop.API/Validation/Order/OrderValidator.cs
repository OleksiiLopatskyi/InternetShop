using FluentValidation;
using InternetShop.BAL.DTOs.Order;

namespace InternetShop.API.Validation.Order
{
    public class OrderValidator : AbstractValidator<OrderDTO>
    {
        public OrderValidator()
        {
            RuleFor(o => o.Products).NotNull();
            RuleForEach(o => o.Products).SetValidator(new OrderProductValidator());
            When(p => p.Products != null, () =>
              {
                  RuleFor(p => p.Products)
                  .Must(p => p.Count() > 0 && p.Count() <= 50)
                  .WithMessage("Order should contain at least one product");
              });
        }
    }
}
