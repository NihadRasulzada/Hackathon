using FluentValidation;

namespace Application.DTOs.AuthServiceDTOs
{
    public class VerifyResetTokenAuthServiceDto
    {
        public string ResetToken { get; set; }
        public string UserId { get; set; }
    }

    public class VerifyResetTokenAuthServiceDtoValidator : AbstractValidator<VerifyResetTokenAuthServiceDto>
    {
        public VerifyResetTokenAuthServiceDtoValidator()
        {
            // ResetToken validation
            RuleFor(x => x.ResetToken)
                .NotEmpty().WithMessage("Reset token field cannot be empty.")
                .Matches(@"^[A-Za-z0-9\-._~+/=]+$").WithMessage("Reset token format is invalid.");

            // UserId validation (GUID format)
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID field cannot be empty.")
                .Must(BeAValidGuid).WithMessage("User ID must be a valid GUID.");
        }

        // Custom method to validate GUID format
        private bool BeAValidGuid(string userId)
        {
            return Guid.TryParse(userId, out _);
        }
    }
}
