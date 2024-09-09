using ClinicManager.Application.Services.ServicesEmail;
using ClinicManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ClinicManager.Application.Services
{
    public class EmailReminderService
    {
        private readonly IServiceProvider _serviceProvider;

        public EmailReminderService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task SendEmailRemindersAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ClinicDbContext>();
                var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();

                var targetTime = DateTime.Now.AddMinutes(5);
                var now = DateTime.Now;

                var appointments = await dbContext.CustomerServices
                    .Include(c => c.Patient)
                    .Where(c => c.Start >= now && c.Start <= targetTime)
                    .ToListAsync();

                foreach (var appointment in appointments)
                {
                    var patient = appointment.Patient;
                    var subject = "Lembrete da consulta";
                    var message = $"Olá {patient.Name}, este é um lembrete de sua consulta marcada para {appointment.Start}.";

                    var emailResult = await emailSender.SendEmailAsync(patient.Email, subject, message);

                    if (!emailResult.IsSucess)
                    {
                        Console.WriteLine($"Erro ao enviar e-mail: {emailResult.Message}");
                    }
                }
            }
        }
    }
}
