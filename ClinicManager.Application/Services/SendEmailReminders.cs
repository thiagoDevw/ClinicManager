using ClinicManager.Application.Services.ServicesEmail;
using ClinicManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
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

        public async Task SendEmailReminders()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ClinicDbContext>();
                var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();

                var now = DateTime.Now;

                var reminderTime = now.AddMinutes(5);

                var appointments = await dbContext.CustomerServices
                    .Include(c => c.Patient)
                    .Where(c => c.Start >= now && c.Start <= reminderTime && !c.ReminderSent)
                    .ToListAsync();

                foreach (var appointment in appointments)
                {
                    var reminderDateTime = appointment.Start.AddMinutes(-5);

                    var patient = appointment.Patient;
                    var subject = "Lembrete da consulta";
                    var message = $"Olá {patient.Name}, este é um lembrete de sua consulta marcada para {appointment.Start}.";

                    var emailResult = await emailSender.SendEmailAsync(patient.Email, subject, message);

                    if (emailResult.IsSucess)
                    {
                        appointment.ReminderSent = true;
                        await dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        Console.WriteLine($"Erro ao enviar e-mail: {emailResult.Message}");
                    }
                }
            }
        }
    }
}


