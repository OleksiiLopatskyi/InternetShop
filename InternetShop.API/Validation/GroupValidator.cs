using FluentValidation;
using InternetShop.BAL.DTOs.Group;

namespace InternetShop.API.Validation
{
    public class GroupValidator : AbstractValidator<GroupDTO>
    {
        public GroupValidator()
        {
            RuleFor(g=>g.Name).NotNull().NotEmpty().Length(5,20);
            RuleFor(g => g.Description).NotEmpty();
            When(g => g.Description.Length > 0, () =>
            {
                RuleFor(g => g.Description).Length(20,100);
            });
        }
    }
}
