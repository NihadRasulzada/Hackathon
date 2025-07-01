using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Services
{
    public interface IOtpService
    {
        Task<string> SendOtpNumberAsync(string phoneNumber);
        Task<string> SendOtpEmailAsync(string email);
    }
}
