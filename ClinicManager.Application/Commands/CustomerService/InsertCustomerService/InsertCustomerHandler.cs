using ClinicManager.Application.Commands.CommandsCustomerService.CreateCustomerService;
using ClinicManager.Application.Models;
using ClinicManager.Application.Services.ServicesEmail;
using ClinicManager.Core.Entities;
using ClinicManager.Infrastructure.Persistence;
using MediatR;

namespace ClinicManager.Application.Commands.CommandsCustomerService.InsertCustomerService
{
    internal class InsertCustomerHandler : IRequestHandler<InsertCustomerServiceCommand, ResultViewModel<int>>
    {
        private readonly ClinicDbContext _context;
        private readonly IEmailSender _emailSender;

        public InsertCustomerHandler(ClinicDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        public async Task<ResultViewModel<int>> Handle(InsertCustomerServiceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var customerService = new CustomerService
                {
                    PatientId = request.PatientId,
                    ServiceId = request.ServiceId,
                    DoctorId = request.DoctorId,
                    Agreement = request.Agreement,
                    TypeService = request.TypeService,
                    Start = request.Start,
                    End = request.End
                };

                _context.CustomerServices.Add(customerService);
                await _context.SaveChangesAsync(cancellationToken);

                // Buscar o paciente pelo PatientId
                var patient = _context.Patients.FirstOrDefault(p => p.Id == request.PatientId);

                if (patient == null)
                {
                    return ResultViewModel<int>.Error("Paciente não encontrado.");
                }

                // Obter o email do paciente
                string toEmail = patient.Email;

                // Definir assunto e mensagem de email
                string subject = "Novo atendimento criado";
                string message = $"Olá {patient.Name}, seu novo atendimento foi criado com sucesso para o dia {customerService.Start}.";

                // Criar o link do Google Calendar
                string startDate = customerService.Start.ToString("yyyyMMddTHHmmss");
                string endDate = customerService.End.ToString("yyyyMMddTHHmmss");

                string googleCalendarUrl = $"https://www.google.com/calendar/render?action=TEMPLATE&text=Consulta+com+{patient.Name}&dates={startDate}/{endDate}&details=Consulta+com+o+Dr.+{request.DoctorId}&location=Clinica&ctz=America/Sao_Paulo&sf=true&output=xml";

                // Adicionar o link do Google Calendar ao email
                message += $"\n\nClique aqui para adicionar ao Google Calendar: <a href='{googleCalendarUrl}'>Adicionar ao Google Calendar</a>";

                // Enviar o email
                var emailResult = await _emailSender.SendEmailAsync(toEmail, subject, message);

                if (!emailResult.IsSucess)
                {
                    Console.WriteLine($"Erro ao enviar email: {emailResult.Message}");
                    return ResultViewModel<int>.Error($"Atendimento criado, mas houve um erro ao enviar o email: {emailResult.Message}");
                }
                _context.SaveChanges();

                return ResultViewModel<int>.Success(customerService.Id);
            }
            catch (Exception ex)
            {
                return ResultViewModel<int>.Error($"Erro ao criar o atendimento: {ex.Message}");
            }
        }
    }
}
