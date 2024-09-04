using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace ClinicManager.Application.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly SendGridClient _sendGridClient;

        public EmailSender(SendGridClient sendGridClient)
        {
            _sendGridClient = sendGridClient;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var from = new EmailAddress("thialexandre.tec@gmail.com", "Clinica Teste");
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, body, body);
            var response = await _sendGridClient.SendEmailAsync(msg);
        }
    }
}
