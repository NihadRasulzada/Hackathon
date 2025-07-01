using Application.DTOs.ReservationDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.ReservationValidator
{
    public class UpdateReservationValidator : AbstractValidator<UpdateReservationDTOs>
    {
        public UpdateReservationValidator()
        {
            
        }
    }
}
