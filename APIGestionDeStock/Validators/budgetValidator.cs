using FluentValidation;

namespace APIGestionDeStock.Validators
{
    public class budgetValidator : AbstractValidator<int>
    {
        public budgetValidator()
        {
            RuleFor(num => num).InclusiveBetween(1, 1000000).WithMessage("The value entered must be between 1 and 1000000")
                .Must(value => int.TryParse(value.ToString(), out int _)).WithMessage("The value entered must be an integer.");
        }
    }
}
