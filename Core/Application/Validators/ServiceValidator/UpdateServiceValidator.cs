using Application.DTOs.ServiceDTOs;
using FluentValidation;

namespace Application.Validators.ServiceValidator
{
    public class UpdateServiceValidator : AbstractValidator<UpdateServiceDTOs>
    {
        public UpdateServiceValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Xidmət adı boş olmamalıdır.")
            .MaximumLength(100).WithMessage("Xidmət adı maksimum 100 simvol ola bilər.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Xidmət qiyməti 0-dan böyük olmalıdır.");
        }
    }
}
