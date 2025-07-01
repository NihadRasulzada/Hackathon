using Application.DTOs.ReservationDTOs;
using FluentValidation;

namespace Application.Validators.ReservationValidator
{
    public class UpdateReservationValidator : AbstractValidator<UpdateReservationDTOs>
    {
        public UpdateReservationValidator()
        {

        }
    }
}
