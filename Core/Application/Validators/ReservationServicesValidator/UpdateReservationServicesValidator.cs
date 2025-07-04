﻿using Application.DTOs.ReservationServiceDTOs;
using FluentValidation;

namespace Application.Validators.ReservationServicesValidator
{
    public class UpdateReservationServicesValidator : AbstractValidator<UpdateReservationServiceDTOs>
    {
        public UpdateReservationServicesValidator()
        {
            RuleFor(x => x.ReservationId)
                .NotEmpty().WithMessage("Rezervasiya ID boş olmamalıdır.")
                .Length(1, 50).WithMessage("Rezervasiya ID-nin uzunluğu 1 ilə 50 simvol arasında olmalıdır.");

            RuleFor(x => x.ServiceId)
                .NotEmpty().WithMessage("Xidmət ID boş olmamalıdır.")
                .Length(1, 50).WithMessage("Xidmət ID-nin uzunluğu 1 ilə 50 simvol arasında olmalıdır.");
        }
    }
}
