using FluentValidation;

namespace Persistence.DTOs.UserServiceDTOs
{
    public class CreateUserUserServiceDTOs
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FinCode { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }

    public class CreateUserUserServiceDTOsValidator : AbstractValidator<CreateUserUserServiceDTOs>
    {
        public CreateUserUserServiceDTOsValidator()
        {
            // Name validation
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name field cannot be empty.")
                .Matches(@"^[a-zA-Z\s]+$").WithMessage("Name must contain only letters and spaces.");

            // Surname validation
            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Surname field cannot be empty.")
                .Matches(@"^[a-zA-Z\s]+$").WithMessage("Surname must contain only letters and spaces.");

            // Username validation
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username field cannot be empty.")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters long.")
                .Matches(@"^[a-zA-Z0-9_]+$").WithMessage("Username can only contain letters, numbers, and underscores.");

            // Email validation
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email field cannot be empty.")
                .EmailAddress().WithMessage("Please enter a valid email address.");

            // Password validation
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password field cannot be empty.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
                .Matches(@"[\W]").WithMessage("Password must contain at least one special character.");

            // PasswordConfirm validation
            RuleFor(x => x.PasswordConfirm)
                .NotEmpty().WithMessage("Password confirmation field cannot be empty.")
                .Equal(x => x.Password).WithMessage("Password and confirmation must match.");

            RuleFor(x => x.PhoneNumber)
    .NotEmpty().WithMessage("Phone number field cannot be empty.")
    .Matches(@"^\+994(50|51|55|70|77|99)\d{7}$")
    .WithMessage("Phone number must be in the format +99450XXXXXXXX, where X is a digit.");

        }
    }
}