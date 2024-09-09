using ClinicManager.Application.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace ClinicManager.Application.Services.ServicesEmail
{
    public class EmailSender : IEmailSender
    {
        private readonly SendGridClient _sendGridClient;

        public EmailSender(SendGridClient sendGridClient)
        {
            _sendGridClient = sendGridClient;
        }


        public async Task<ResultViewModel> SendEmailAsync(string toEmail, string subject, string body)
        {
            if (string.IsNullOrWhiteSpace(toEmail))
            {
                throw new ArgumentNullException(nameof(toEmail), "O endereço de e-mail não pode ser nulo ou vazio.");
            }

            if (string.IsNullOrWhiteSpace(subject))
            {
                throw new ArgumentNullException(nameof(subject), "O assunto do e-mail não pode ser nulo ou vazio.");
            }

            if (string.IsNullOrWhiteSpace(body))
            {
                throw new ArgumentNullException(nameof(body), "O corpo do e-mail não pode ser nulo ou vazio.");
            }

            try
            {
                var from = new EmailAddress("thialexandre.tec@gmail.com", "Clinica Teste");
                var to = new EmailAddress(toEmail);
                var msg = MailHelper.CreateSingleEmail(from, to, subject, body, body);

                var response = await _sendGridClient.SendEmailAsync(msg);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return ResultViewModel.Success();
                }

                return ResultViewModel.Error($"Erro ao enviar o email. Código de status: {response.StatusCode}");

            }
            catch (Exception ex)
            {
                return ResultViewModel.Error($"Erro ao enviar o email: {ex.Message}");
            }
        }
    }
}
