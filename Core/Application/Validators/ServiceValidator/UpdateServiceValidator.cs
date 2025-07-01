using Application.DTOs.ServiceDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
