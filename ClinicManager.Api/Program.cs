using ClinicManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using ClinicManager.Application;
using Hangfire;
using ClinicManager.Application.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddControllers();
    

//builder.Services.AddDbContext<ClinicDbContext>(o => o.UseInMemoryDatabase("ClinicManagerDb"));
var connectionString = 
    builder.Configuration.GetConnectionString("ClinicManagerCs");

var connectionStringHangfire = 
    builder.Configuration.GetConnectionString("HangfireConnection");

builder.Services.AddHangfire((sp, config) =>
{
    config.UseSqlServerStorage(connectionStringHangfire);
});

builder.Services.AddHangfireServer();

builder.Services.AddDbContext<ClinicDbContext>(o => o.UseSqlServer(connectionString));

builder.Services.AddMemoryCache();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication(builder.Configuration);

var app = builder.Build();

app.UseHangfireDashboard();

RecurringJob.AddOrUpdate<EmailReminderService>(
    "send-email-reminders",
    service => service.SendEmailReminders(),
    Cron.MinuteInterval(1)
);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public class JsonDateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateTime.Parse(reader.GetString());
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("yyyy-MM-dd")); // Formato de data desejado
    }
}
