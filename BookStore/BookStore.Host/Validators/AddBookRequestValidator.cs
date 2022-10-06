using BookStore.Models.Requests;
using FluentValidation;

namespace BookStore.Host.Validators
{
    public class AddBookRequestValidator :AbstractValidator<AddBookRequest>
    {
        public AddBookRequestValidator()
        {
            RuleFor(x => x.AuthorName).NotEmpty().MinimumLength(2).MaximumLength(200);
            RuleFor(x => x.Title).NotEmpty().MaximumLength(200).MinimumLength(1);
        }
    }
}
