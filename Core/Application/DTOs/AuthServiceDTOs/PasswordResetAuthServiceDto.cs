using FluentValidation;

namespace Application.DTOs.AuthServiceDTOs
{
    public class PasswordResetAuthServiceDto
    {
        public string Email { get; set; }
    }

    public class PasswordResetAuthServiceDtoValidator : AbstractValidator<PasswordResetAuthServiceDto>
    {
        public PasswordResetAuthServiceDtoValidator()
        {
            // Email validation
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email field cannot be empty.")
                .EmailAddress().WithMessage("Please enter a valid email address.");
        }
    }
}
