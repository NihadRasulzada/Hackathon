namespace Application.DTOs.AuthServiceDTOs.Response
{
    public class LoginAuthServiceResponseDto
    {
        public bool IsPhoneVerified { get; set; }
        public bool IsEmailVerified { get; set; }
        public bool TwoFactorRequired { get; set; }
    }
}
