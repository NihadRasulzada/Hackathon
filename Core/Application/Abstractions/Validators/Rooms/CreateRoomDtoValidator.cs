using Application.DTOs.Rooms;
using FluentValidation;

namespace Application.Abstractions.Validators.Rooms
{
    public class CreateRoomDtoValidator : AbstractValidator<CreateRoomDto>
    {
        public CreateRoomDtoValidator()
        {
            RuleFor(x => x.Number)
                .GreaterThan(0).WithMessage("Otaq nömrəsi 0-dan böyük olmalıdır.");

            RuleFor(x => x.PricePerNight)
                .GreaterThan(0).WithMessage("Qiymət 0-dan böyük olmalıdır.");

            RuleFor(x => x.RoomType)
                .IsInEnum().WithMessage("Yanlış otaq tipi seçildi.");
        }
    }
}
