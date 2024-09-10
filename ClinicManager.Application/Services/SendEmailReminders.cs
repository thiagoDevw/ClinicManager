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
        private readonly IMemoryCache _cache;


        public EmailReminderService(IServiceProvider serviceProvider, IMemoryCache cache)
        {
            _serviceProvider = serviceProvider;
            _cache = cache;
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
                    .Where(c => c.Start >= now && c.Start <= reminderTime)
                    .ToListAsync();

                foreach (var appointment in appointments)
                {
                    var reminderDateTime = appointment.Start.AddMinutes(-5);
                    var cacheKey = $"ReminderSent_{appointment.Id}";

                    if (_cache.TryGetValue(cacheKey, out bool isReminderSent) && isReminderSent)
                    {
                        continue; // Skip sending if the reminder was already sent
                    }

                    var patient = appointment.Patient;
                    var subject = "Lembrete da consulta";
                    var message = $"Olá {patient.Name}, este é um lembrete de sua consulta marcada para {appointment.Start}.";

                    var emailResult = await emailSender.SendEmailAsync(patient.Email, subject, message);

                    if (!emailResult.IsSucess)
                    {
                        Console.WriteLine($"Erro ao enviar e-mail: {emailResult.Message}");
                    }
                    else
                    {
                        // Set cache entry to indicate the reminder has been sent
                        _cache.Set(cacheKey, true); // Cache expiration time
                    }

                }
            }
        }
    }
}


