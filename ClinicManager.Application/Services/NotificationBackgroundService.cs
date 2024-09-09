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

        public NotificationBackgroundService(IServiceProvider serviceProvider, IEmailSender emailSender)
        {
            _serviceProvider = serviceProvider;
            _emailSender = emailSender;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() => BackgroundProcessing(stoppingToken), stoppingToken);
        }

        private void BackgroundProcessing(CancellationToken stoppingToken)
        {
            Console.WriteLine("Serviço de background iniciado...");

            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("Executando o serviço de Background...");

                // Rodar a cada 24 horas
                Thread.Sleep(TimeSpan.FromMinutes(5));

                // Executar a lógica de notificação
                NotifyPatients();
            }
        }

        private void NotifyPatients()
        {
            Console.WriteLine("Notificando paciente");

            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ClinicDbContext>();
                var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();

                // Buscar os atendimentos do dia seguinte
                var now = DateTime.Now;
                var targetTime = now.AddMinutes(5);

                var appointments = dbContext.CustomerServices
                    .Include(c => c.Patient)
                    .Where(c => c.Start >= DateTime.Now && c.Start <= targetTime)
                    .ToList();

                Console.WriteLine($"Número de atendimentos encontrados: {appointments.Count}");

                foreach (var appointment in appointments)
                {
                    var patient = appointment.Patient;
                    Console.WriteLine($"Notificando paciente: {patient.Name}");
                    var subject = "Lembrete da consulta";
                    var message = $"Olá {patient.Name}, este é um lembrete de sua consulta marcada para {appointment.Start}.";

                    // Enviar o email de notificação
                    var emailResult = emailSender.SendEmail(patient.Email, subject, message);

                    if (!emailResult.IsSucess)
                    {
                        Console.WriteLine($"Erro ao enviar email: {emailResult.Message}");
                    }
                }
            }
        }
    }
}
