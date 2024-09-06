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
            while (!stoppingToken.IsCancellationRequested)
            {
                // Rodar a cada 24 horas
                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);

                // Executar a lógica de notificação
                await NotifyPatientsAsync();
            }

            throw new NotImplementedException();

            
        }

        private async Task NotifyPatientsAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ClinicDbContext>;
                var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();

                // Buscar os atendimentos do dia seguinte
                var tomorrow = DateTime.Today.AddDays(1);
                /*var appointments = dbContext.CustomerServices
                    .Include(c => c.Patient)
                    .Where(c => c.Start.Date == tomorrow)
                    .ToList();
                */
            }
        }
    }
}
