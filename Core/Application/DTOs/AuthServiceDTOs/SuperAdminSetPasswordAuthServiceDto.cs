namespace Application.DTOs.AuthServiceDTOs
{
    public class SuperAdminSetPasswordAuthServiceDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
