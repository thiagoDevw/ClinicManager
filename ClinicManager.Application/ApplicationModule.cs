using ClinicManager.Application.Services;
using ClinicManager.Application.Services.ServicesCustomer;
using ClinicManager.Application.Services.ServicesDoctor;
using ClinicManager.Application.Services.ServicesPatient;
using ClinicManager.Application.Services.ServicesService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SendGrid;


namespace ClinicManager.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services
                .AddServices();

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerServiceManager>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IService, ServiceManager>();

            services.AddSingleton<IEmailSender>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var apiKey = configuration["SendGrid:Apikey"];
                var sendGridClient = new SendGridClient(apiKey);
                return new EmailSender(sendGridClient);
            });

            return services;
        }

    }
}
