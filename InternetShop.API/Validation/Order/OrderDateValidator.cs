using FluentValidation;
using InternetShop.BAL.DTOs.Order;

namespace InternetShop.API.Validation.Order
{
    public class OrderDateValidator : AbstractValidator<OrderDateDTO>
    {
        public OrderDateValidator()
        {
            RuleFor(d=>d.OrderDate).NotNull().NotEmpty();
            RuleFor(d=>d.ReceiveDate).NotNull().NotEmpty()
                .LessThan(DateTime.Now.AddDays(1)); ;
        }
    }
}
