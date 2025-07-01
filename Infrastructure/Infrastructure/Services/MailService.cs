using Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Infrastructure.Services
{
    public class MailService : IMailService
    {
        readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendMailAsync(new[] { to }, subject, body, isBodyHtml);
        }

        public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            // MailMessage object creation
            var mail = new MailMessage
            {
                IsBodyHtml = isBodyHtml,
                Subject = subject,
                Body = body,
                From = new MailAddress(_configuration["Mail:Username"], "Hackathon", Encoding.UTF8)
            };

            // Add recipients
            foreach (var to in tos)
                mail.To.Add(to);

            using (var smtp = new SmtpClient())
            {
                smtp.Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]);
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Host = _configuration["Mail:Host"];

                await smtp.SendMailAsync(mail);
            }
        }

        public async Task SendPasswordResetMailAsync(string to, string userId, string resetToken)
        {
            StringBuilder mail = new();
            mail.AppendLine("Hello<br>If you requested a new password, you can reset it using the link below.<br><strong><a target=\"_blank\" href=\"");
            mail.AppendLine(_configuration["FrontClientUrl"]);
            mail.AppendLine("/update-password/");
            mail.AppendLine(userId.ToString());
            mail.AppendLine("/");
            mail.AppendLine(resetToken);
            mail.AppendLine("\">Click here to reset your password...</a></strong><br><br><span style=\"font-size:12px;\">NOTE: If you did not make this request, please disregard this email.</span><br>Best regards...<br><br><br>NG - Mini|E-Commerce");

            await SendMailAsync(to, "Password Reset Request", mail.ToString());
        }

        public async Task SendCompletedOrderMailAsync(string to, string orderCode, DateTime orderDate, string userName)
        {
            string mail = $"Dear {userName},<br>" +
                "Your order with order code " + $"{orderCode}" + " placed on " + $"{orderDate:MMMM dd, yyyy}" + " has been completed and handed over to the courier company.<br>We hope you enjoy your purchase...";

            await SendMailAsync(to, $"Your Order {orderCode} Has Been Completed", mail);
        }


    }
}
