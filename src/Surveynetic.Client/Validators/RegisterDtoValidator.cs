using FluentValidation;
using Surveynetic.Shared.DTO.Account;

namespace Surveynetic.Client.Validators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(d => d.UserName)
                .NotNull().WithMessage("Username is required")
                .MinimumLength(3).WithMessage("Username is too short")
                .MaximumLength(24).WithMessage("Username is too long");
            RuleFor(d => d.Email)
                .NotNull().WithMessage("E-mail address is required")
                .EmailAddress().WithMessage("E-mail address is not valid");
            RuleFor(d => d.Password)
                .NotNull().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password is too short")
                .MaximumLength(128).WithMessage("Password is too long");
        }
    }
}
