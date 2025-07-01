using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.AuthServiceDTOs.Response
{
    public class LoginResponseDto
    {
        public LoginAuthServiceResponseDto LoginInfo { get; set; }
        public Token Token { get; set; }
    }
}
