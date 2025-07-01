using FluentValidation;

namespace Application.DTOs.AuthServiceDTOs
{
    public class RefreshTokenLoginAuthServiceDto
    {
        public string RefreshToken { get; set; }
    }

    public class RefreshTokenLoginAuthServiceDtoValidator : AbstractValidator<RefreshTokenLoginAuthServiceDto>
    {
        public RefreshTokenLoginAuthServiceDtoValidator()
        {
            // RefreshToken validation
            RuleFor(x => x.RefreshToken)
                .NotEmpty().WithMessage("Refresh token field cannot be empty.");
        }
    }
}
