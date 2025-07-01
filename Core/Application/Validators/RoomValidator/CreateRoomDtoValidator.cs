using Application.DTOs.Rooms;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.RoomValidator
{
    public class CreateRoomDtoValidator:AbstractValidator<CreateRoomDto>
    {
        public CreateRoomDtoValidator()
        {
            RuleFor(x => x.Number)
                  .GreaterThan(0).WithMessage("Otaq nömrəsi 0-dan böyük olmalıdır.");

            RuleFor(x => x.PricePerNight)
                .GreaterThan(0).WithMessage("Qiymət 0-dan böyük olmalıdır.");

            RuleFor(x => x.RoomType)
                .IsInEnum().WithMessage("Yanlış otaq tipi seçildi.");
            RuleFor(x => x.RoomType)
            .IsInEnum()
            .WithMessage("Düzgün otaq tipi seçin (Single, Double, Suite).");
        }
    }
}
