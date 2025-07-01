using Application.Abstractions.Services;
using Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class OtpService : IOtpService
    {
        readonly ISmsService _smsService;
        readonly IMailService _mailService;

        public OtpService(ISmsService smsService, IMailService mailService)
        {
            _smsService = smsService;
            _mailService = mailService;
        }

        public async Task<string> SendOtpNumberAsync(string phoneNumber)
        {
            var otp = OTPGenerator.GenerateOtp();

            await _smsService.SendSmsAsync(phoneNumber, $"Your OTP is: {otp}");

            return otp;
        }

        public async Task<string> SendOtpEmailAsync(string email)
        {
            var otp = OTPGenerator.GenerateOtp();
            StringBuilder mail = new();
            mail.AppendLine("Hello,<br>Your OTP is: <strong>" + otp + "</strong><br>Please use this code to verify your email.<br>Best regards...<br><br>");
            await _mailService.SendMailAsync(email, "Your OTP Code", mail.ToString());
            return otp;
        }
    }
}
