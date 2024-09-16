using ClinicManager.Application.Commands.CommandsCustomerService.CreateCustomerService;
using ClinicManager.Application.Services;
using ClinicManager.Application.Services.ServicesCalendar;
using ClinicManager.Application.Services.ServicesEmail;
using ClinicManager.Application.Services.ServicesPatient;
using ClinicManager.Application.Services.ServicesService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SendGrid;


namespace ClinicManager.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddServices(configuration)
                .AddHandlers();


            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IService, ServiceManager>();
            services.AddScoped<EmailReminderService>();

            // Configuração do Email com SendGrid
            services.AddSingleton<IEmailSender>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var apiKey = configuration["SendGrid:Apikey"];
                var sendGridClient = new SendGridClient(apiKey);
                return new EmailSender(sendGridClient);
            });

            // Configuração do Google Calendar Service
            services.AddScoped<GoogleCalendarService>(provider =>
            {
                var config = provider.GetRequiredService<IConfiguration>();
                return new GoogleCalendarService(config);
            });

            return services;
        }

        private static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddMediatR(config =>
                config.RegisterServicesFromAssemblyContaining<InsertCustomerServiceCommand>());
            return services;
        }
    }
}
