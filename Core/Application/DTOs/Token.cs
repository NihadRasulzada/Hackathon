using FluentValidation;

namespace Application.DTOs
{
    public class Token
    {
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
        public string RefreshToken { get; set; }
    }

    public class TokenValidator : AbstractValidator<Token>
    {
        public TokenValidator()
        {
            // AccessToken validation
            RuleFor(x => x.AccessToken)
                .NotEmpty().WithMessage("Access token field cannot be empty.")
                .Matches(@"^[A-Za-z0-9\-._~+/=]+$").WithMessage("Access token format is invalid.");

            // Expiration validation
            RuleFor(x => x.Expiration)
                .GreaterThan(DateTime.UtcNow.AddHours(4)).WithMessage("Expiration date must be in the future.");

            // RefreshToken validation
            RuleFor(x => x.RefreshToken)
                .NotEmpty().WithMessage("Refresh token field cannot be empty.")
                .Matches(@"^[A-Za-z0-9\-._~+/=]+$").WithMessage("Refresh token format is invalid.");
        }
    }
}
