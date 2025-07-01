using FluentValidation;

namespace Application.DTOs.AuthServiceDTOs
{
    public class LoginSendOtpToNumberAuthServiceDto
    {
        public string Number { get; set; }
    }

    public class LoginSendOtpToNumberAuthServiceDtoValidator : AbstractValidator<LoginSendOtpToNumberAuthServiceDto>
    {
        public LoginSendOtpToNumberAuthServiceDtoValidator()
        {
            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("Phone number field cannot be empty.")
                .Matches(@"^\+994(50|51|55|70|77|99)\d{7}$")
                .WithMessage("Phone number must be in the format +99450XXXXXXXX, where X is a digit.");
        }
    }
}
