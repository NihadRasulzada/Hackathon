using FluentValidation;

namespace Application.DTOs.UserServiceDTOs
{
    public class GetAllUsersUserSeviceDTOs
    {
        public int Page { get; set; }
        public int Size { get; set; }
    }

    public class GetAllUsersUserSeviceDTOsValidator : AbstractValidator<GetAllUsersUserSeviceDTOs>
    {
        public GetAllUsersUserSeviceDTOsValidator()
        {
            // Page validation
            RuleFor(x => x.Page)
                .GreaterThan(-1).WithMessage("Page must be greater than 0.");

            // Size validation
            RuleFor(x => x.Size)
                .GreaterThan(-1).WithMessage("Size must be greater than 0.")
                .LessThanOrEqualTo(100).WithMessage("Size must be less than or equal to 100.");
        }
    }
}
