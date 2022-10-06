using BookStore.Models.Requests;
using FluentValidation;

namespace BookStore.Host.Validators
{
    public class TestRequestValidator : AbstractValidator<ValidatorTestRequest>
    {
        public TestRequestValidator()
        {
            RuleForEach(x => x.MyCollection)
                .ChildRules(myClassRules =>
                {
                    myClassRules.RuleFor(x => x.Name).NotEmpty();
                });
        }
    }
}
