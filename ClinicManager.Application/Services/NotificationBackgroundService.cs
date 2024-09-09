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

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Serviço de background iniciado...");

            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("Executando o serviço de Background...");

                // Rodar a cada 24 horas
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);

                // Executar a lógica de notificação
                await NotifyPatientsAsync(stoppingToken);
            }
        }

        private async Task NotifyPatientsAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Notificando paciente");

            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ClinicDbContext>();
                var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();

                // Buscar os atendimentos do dia seguinte
                
                var targetTime = DateTime.Now.AddMinutes(5);
                var now = DateTime.Now;

                var appointments = dbContext.CustomerServices
                    .Include(c => c.Patient)
                    .Where(c => c.Start >= now && c.Start <= targetTime)
                    .ToList();

                Console.WriteLine($"Número de atendimentos encontrados: {appointments.Count}");

                foreach (var appointment in appointments)
                {
                    if (stoppingToken.IsCancellationRequested)
                    {
                        Console.WriteLine("Serviço de background cancelado.");
                        return;
                    }

                    var patient = appointment.Patient;
                    Console.WriteLine($"Notificando paciente: {patient.Name}");
                    var subject = "Lembrete da consulta";
                    var message = $"Olá {patient.Name}, este é um lembrete de sua consulta marcada para {appointment.Start}.";

                    // Enviar o email de notificação
                    var emailResult = await emailSender.SendEmailAsync(patient.Email, subject, message);

                    if (!emailResult.IsSucess)
                    {
                        Console.WriteLine($"Erro ao enviar email: {emailResult.Message}");
                    }
                }
            }
        }
    }
}
