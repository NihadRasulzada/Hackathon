namespace Application.DTOs.AuthServiceDTOs.Response
{
    public class LoginResponseDto
    {
        public LoginAuthServiceResponseDto LoginInfo { get; set; }
        public Token Token { get; set; }
    }
}
