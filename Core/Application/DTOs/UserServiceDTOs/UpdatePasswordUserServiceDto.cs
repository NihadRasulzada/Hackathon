using FluentValidation;

namespace Persistence.DTOs.UserServiceDTOs
{
    public class UpdatePasswordUserServiceDto
    {
        public string UserId { get; set; }
        public string ResetToken { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }

    public class UpdatePasswordUserServiceDtoValidator : AbstractValidator<UpdatePasswordUserServiceDto>
    {
        public UpdatePasswordUserServiceDtoValidator()
        {
            // UserId validation (GUID format)
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID field cannot be empty.")
                .Must(BeAValidGuid).WithMessage("User ID must be a valid GUID.");

            // ResetToken validation
            RuleFor(x => x.ResetToken)
                .NotEmpty().WithMessage("Reset token field cannot be empty.")
                .Matches(@"^[A-Za-z0-9\-._~+/=]+$").WithMessage("Reset token format is invalid.");

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
        }

        // Custom method to validate GUID format
        private bool BeAValidGuid(string userId)
        {
            return Guid.TryParse(userId, out _);
        }
    }
}
