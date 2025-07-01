using Application.DTOs.ReservationDTOs;
using FluentValidation;

namespace Application.Validators.ReservationValidator
{
    public class UpdateReservationValidator : AbstractValidator<UpdateReservationDTOs>
    {
        public UpdateReservationValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("Müştəri ID boş ola bilməz.")
                .MaximumLength(50).WithMessage("Müştəri ID 50 simvoldan uzun ola bilməz.");

            RuleFor(x => x.RoomId)
                .NotEmpty().WithMessage("Otaq ID boş ola bilməz.");
                

            RuleFor(x => x.CheckInDate)
                .NotEmpty().WithMessage("Giriş tarixi qeyd olunmalıdır.")
                .Must(date => date >= DateTime.Today).WithMessage("Giriş tarixi bu gün və ya gələcəkdə olmalıdır.");

            RuleFor(x => x.CheckOutDate)
                .NotEmpty().WithMessage("Çıxış tarixi qeyd olunmalıdır.")
                .GreaterThan(x => x.CheckInDate).WithMessage("Çıxış tarixi giriş tarixindən sonra olmalıdır.");

        }
    }
}
