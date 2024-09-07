using ClinicManager.Application.Services.ServicesEmail;
using ClinicManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ClinicManager.Application.Services
{
    public class NotificationBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IEmailSender _emailSender;
        private readonly ClinicDbContext _dbContext;

        public NotificationBackgroundService(IServiceProvider serviceProvider, IEmailSender emailSender, ClinicDbContext dbContext)
        {
            _serviceProvider = serviceProvider;
            _emailSender = emailSender;
            _dbContext = dbContext;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() => BackgroundProcessing(stoppingToken), stoppingToken);
        }

        private void BackgroundProcessing(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Rodar a cada 24 horas
                Thread.Sleep(TimeSpan.FromHours(24));

                // Executar a lógica de notificação
                NotifyPatients();
            }
        }

        private void NotifyPatients()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ClinicDbContext>;
                var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();

                // Buscar os atendimentos do dia seguinte
                var tomorrow = DateTime.Today.AddDays(1);
                var appointments = _dbContext.CustomerServices
                    .Include(c => c.Patient)
                    .Where(c => c.Start.Date == tomorrow)
                    .ToList();
                
                foreach (var appointment in appointments)
                {
                    var patient = appointment.Patient;
                    var subject = "Lembrete da consulta";
                    var message = $"Olá {patient.Name}, este é um lembrete de sua consulta marcada para {appointment.Start}.";

                    // Enviar o email de notificação
                    var emailResult = emailSender.SendEmail(patient.Email, subject, message);

                    if (!emailResult.IsSucess)
                    {
                        Console.WriteLine($"Erro ao enviar");
                    }
                }
            }
        }
    }
}
