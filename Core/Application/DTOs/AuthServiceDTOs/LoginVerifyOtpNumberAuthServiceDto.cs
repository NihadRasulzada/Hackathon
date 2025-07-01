using FluentValidation;

namespace Application.DTOs.AuthServiceDTOs
{
    public class LoginVerifyOtpNumberAuthServiceDto
    {
        public string Number { get; set; }
        public string Otp { get; set; }
    }
    public class LoginVerifyOtpNumberAuthServiceDtoValidator : AbstractValidator<LoginVerifyOtpNumberAuthServiceDto>
    {
        public LoginVerifyOtpNumberAuthServiceDtoValidator()
        {
            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("Phone number field cannot be empty.")
                .Matches(@"^\+994(50|51|55|70|77|99)\d{7}$")
                .WithMessage("Phone number must be in the format +99450XXXXXXXX, where X is a digit.");

            RuleFor(x => x.Otp)
                .NotEmpty().WithMessage("OTP field cannot be empty.")
                .Matches(@"^\d{6}$").WithMessage("OTP must be a 6-digit number.");
        }
    }
}
