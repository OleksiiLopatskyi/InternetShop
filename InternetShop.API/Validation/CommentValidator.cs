using FluentValidation;
using InternetShop.BAL.DTOs.Comment;

namespace InternetShop.API.Validation
{
    public class CommentValidator : AbstractValidator<CommentDTO>
    {
        public CommentValidator()
        {
            RuleFor(c=>c.ProductId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(c=>c.Text).NotNull().NotEmpty().Length(0,100);
            RuleFor(c=>c.Author).NotNull().NotEmpty();
        }
    }
}
