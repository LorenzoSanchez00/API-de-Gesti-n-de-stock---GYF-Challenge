using APIGestionDeStock.DTOs.Request;
using FluentValidation;
using System.Text.RegularExpressions;

namespace APIGestionDeStock.Validators
{
    public class SignUpValidator : AbstractValidator<UserRequestDTO>
    {
        public SignUpValidator()
        {
            RuleFor(u => u.Name).NotEmpty().WithMessage("Name can´t be null")
                .MaximumLength(50).WithMessage("The Name entered must be between 4 and 50 characters")
                .MinimumLength(4).WithMessage("The Name entered must be between 4 and 50 characters");

            RuleFor(u => u.Email).NotEmpty().WithMessage("Email can´t be null")
                .Must(IsEmailValid).WithMessage("The Email's format is not valid")
                .MaximumLength(100).WithMessage("The email can only have a maximum of 100 characters.");

            RuleFor(u => u.Password).NotEmpty().WithMessage("Password is required")
                .MinimumLength(4).WithMessage("The password can only have a minimum of 4 characters");
        }

        private bool IsEmailValid(string txt)
        {
            var pattern = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return pattern.IsMatch(txt);
        }
    }
}
