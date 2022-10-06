using BookStore.Models.Requests;
using FluentValidation;

namespace BookStore.Host.Validators
{
    public class AddAuthorRequestValidator : AbstractValidator<AddAuthorRequest>
    {

        public AddAuthorRequestValidator()
        {
            RuleFor(x => x.Age).GreaterThan(0);
            RuleFor(x => x.Name.Length).GreaterThan(2);
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.DateOfBirth).GreaterThan(DateTime.MinValue).LessThan(DateTime.MaxValue);
        }
    }
}
