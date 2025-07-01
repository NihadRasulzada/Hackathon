using FluentValidation;

namespace Application.DTOs.AuthServiceDTOs
{
    public class ConfirmEmailAuthServiceDto
    {
        public string UserId { get; set; }
        public string Token { get; set; }
    }

    public class ConfirmEmailAuthServiceDtoValidator : AbstractValidator<ConfirmEmailAuthServiceDto>
    {
        public ConfirmEmailAuthServiceDtoValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId cannot be empty.")
                .Must(BeAValidGuid).WithMessage("UserId must be a valid GUID.");

            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("Token cannot be empty.")
                .Length(6, 100).WithMessage("Token must be between 6 and 100 characters.");
        }

        // GUID doğrulama fonksiyonu
        private bool BeAValidGuid(string userId)
        {
            return Guid.TryParse(userId, out _);
        }
    }
}
