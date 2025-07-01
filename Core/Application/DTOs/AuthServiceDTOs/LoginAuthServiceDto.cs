using FluentValidation;

namespace Application.DTOs.AuthServiceDTOs
{
    public class LoginAuthServiceDto
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }

    public class LoginAuthServiceDtoValidator : AbstractValidator<LoginAuthServiceDto>
    {
        public LoginAuthServiceDtoValidator()
        {
            // UsernameOrEmail validation
            RuleFor(x => x.UsernameOrEmail)
            .NotEmpty().WithMessage("Username or email field cannot be empty.")
            .Must(BeValidUsernameOrEmail).WithMessage("Username or email is not valid.");

            // Password validation
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password field cannot be empty.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
                .Matches(@"[\W]").WithMessage("Password must contain at least one special character.")
                .WithMessage("Password must include uppercase letters, lowercase letters, digits, and special characters.");
        }

        private bool BeValidUsernameOrEmail(string usernameOrEmail)
        {
            // Check if it's a valid email
            if (IsValidEmail(usernameOrEmail))
            {
                return true;
            }

            // Check if it's a valid username (only alphanumeric and underscores, and minimum 3 characters)
            if (IsValidUsername(usernameOrEmail))
            {
                return true;
            }

            return false;
        }

        // Method to check if the value is a valid email
        private bool IsValidEmail(string email)
        {
            // Use FluentValidation's built-in EmailAddress() method
            var emailValidator = new InlineValidator<string>();
            emailValidator.RuleFor(x => x).EmailAddress().NotEmpty();
            var result = emailValidator.Validate(email);
            return result.IsValid;
        }

        // Method to check if the value is a valid username
        private bool IsValidUsername(string username)
        {
            return !string.IsNullOrEmpty(username) && username.Length >= 3 && System.Text.RegularExpressions.Regex.IsMatch(username, @"^[a-zA-Z0-9_]+$");
        }
    }
}
