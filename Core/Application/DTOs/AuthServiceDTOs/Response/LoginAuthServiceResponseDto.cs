using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.AuthServiceDTOs.Response
{
    public class LoginAuthServiceResponseDto
    {
        public bool IsPhoneVerified { get; set; }
        public bool IsEmailVerified { get; set; }
        public bool TwoFactorRequired { get; set; }
    }
}
