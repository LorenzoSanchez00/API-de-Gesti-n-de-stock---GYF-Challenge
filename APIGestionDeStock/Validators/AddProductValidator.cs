using APIGestionDeStock.DTOs.Request;
using FluentValidation;
using System.Text.RegularExpressions;

namespace APIGestionDeStock.Validators
{
    public class AddProductValidator : AbstractValidator<ProductRequestDTO>
    {
        public AddProductValidator()
        {
            RuleFor(p => p.Price).NotEmpty().WithMessage("Price can´t be null")
                .GreaterThanOrEqualTo(0).WithMessage("The value entered can´t be less than 0");

            RuleFor(product => product.Category).IsInEnum().WithMessage("The value of the category must be 0 or 1");

        }

        private bool IsValidDate(DateTime date)
        {
            string dateInString = date.ToString("yyyy-MM-dd");
            var format = new Regex(@"^\d{4}-\d{2}-\d{2}$");

            return format.IsMatch(dateInString);
        }
    }
}
