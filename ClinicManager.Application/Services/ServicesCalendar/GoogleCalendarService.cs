using ClinicManager.Core.Entities;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Microsoft.Extensions.Configuration;

namespace ClinicManager.Application.Services.ServicesCalendar
{
    public class GoogleCalendarService
    {
        private readonly IConfiguration _configuration;
        private static string[] Scopes = { CalendarService.Scope.Calendar };
        private static string ApplicationName = "Clinic Manager";

        public GoogleCalendarService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public CalendarService GetCalendarService()
        {
            var clientId = _configuration["Google:ClientId"];
            var clientSecret = _configuration["Google:ClientSecret"];

            if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret))
            {
                throw new Exception("As credenciais do Google não foram configuradas corretamente.");
            }

            var clientSecrets = new ClientSecrets
            {
                ClientId = clientId,
                ClientSecret = clientSecret
            };

            // Autenticação com as credenciais do cliente
            UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                clientSecrets,
                Scopes,
                "user",
                CancellationToken.None).Result;

            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            return service;
        }

        // Método para criar o evento
        public void CreateEvent(string patientName, DateTime startDate, DateTime endDate)
        {
            var service = GetCalendarService();

            var startDateInLocalTime = TimeZoneInfo.ConvertTime(startDate, TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo"));
            var endDateInLocalTime = TimeZoneInfo.ConvertTime(endDate, TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo"));

            var calendarEvent = new Event
            {
                Summary = $"Consulta de {patientName}",
                Start = new EventDateTime
                {
                    DateTime = startDate,
                    TimeZone = "America/Sao_Paulo"
                },
                End = new EventDateTime
                {
                    DateTime = endDate,
                    TimeZone = "America/Sao_Paulo"
                }
            };

            try
            {
                var request = service.Events.Insert(calendarEvent, "primary");
                var createdEvent = request.Execute();
                Console.WriteLine($"Evento criado com sucesso: {createdEvent.HtmlLink}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar o evento no Google Calendar: {ex.Message}");
            }
        }
    }
}
