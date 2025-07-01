using Application.DTOs.ReservationDTOs;
using FluentValidation;

namespace Application.Validators.ReservationValidator
{
    public class CreateReservationValidator : AbstractValidator<CreateReservationDTOs>
    {
        public CreateReservationValidator()
        {

        }
    }
}
