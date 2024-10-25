using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace infolog_asp_fab.Services // Assurez-vous que le namespace correspond
{
    public class AuthMessageSenderOptions
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPass { get; set; }
    }

    public class EmailSender : IEmailSender
    {
        private readonly AuthMessageSenderOptions _options;

        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            _options = optionsAccessor.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient(_options.SmtpServer)
            {
                Port = _options.SmtpPort,
                Credentials = new NetworkCredential(_options.SmtpUser, _options.SmtpPass),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_options.SmtpUser),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(email);

            await client.SendMailAsync(mailMessage);
        }
    }
}
