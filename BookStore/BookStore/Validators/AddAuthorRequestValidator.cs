using BookStore.Models.Requests;
using FluentValidation;

namespace BookStore.Validators
{
    public class AddAuthorRequestValidator : AbstractValidator<AddAuthorRequest>
    {
        public AddAuthorRequestValidator()
        {
            RuleFor(x => x.Age)
                .GreaterThan(0).WithMessage("My custom message for Age zero")
                .LessThanOrEqualTo(120)
                .WithMessage("My custom message for Age 120");
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);
            When(x => !string.IsNullOrEmpty(x.NickName), () =>
            {
                RuleFor(x => x.NickName)
                    .MinimumLength(2)
                    .MaximumLength(10);
            });
            RuleFor(x => x.DateOfBirth)
                .GreaterThan(DateTime.MinValue)
                .LessThan(DateTime.MaxValue);
        }
    }
}
