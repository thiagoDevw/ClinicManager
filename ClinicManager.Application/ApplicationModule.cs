using ClinicManager.Application.Services;
using ClinicManager.Application.Services.ServicesCalendar;
using ClinicManager.Application.Services.ServicesCustomer;
using ClinicManager.Application.Services.ServicesDoctor;
using ClinicManager.Application.Services.ServicesEmail;
using ClinicManager.Application.Services.ServicesPatient;
using ClinicManager.Application.Services.ServicesService;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SendGrid;
using System.Reflection;


namespace ClinicManager.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddServices(configuration);

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICustomerService, CustomerServiceManager>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IService, ServiceManager>();
            services.AddScoped<EmailReminderService>();
            services.AddMediatR(Assembly.GetExecutingAssembly());

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
    }
}
