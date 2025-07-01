using Application.DTOs.ReservationDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.ReservationValidator
{
    public class CreateReservationValidator : AbstractValidator<CreateReservationDTOs>
    {
        public CreateReservationValidator()
        {
            
        }
    }
}
