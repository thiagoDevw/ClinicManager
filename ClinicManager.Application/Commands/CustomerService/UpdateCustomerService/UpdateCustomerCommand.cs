using ClinicManager.Application.Models;
using ClinicManager.Core.Enums;
using MediatR;

namespace ClinicManager.Application.Commands.CommandsCustomerService.UpdateCustomerService
{
    public class UpdateCustomerCommand : IRequest<ResultViewModel>
    {
        public UpdateCustomerCommand(int id, int patientId, int doctorId, int serviceId, string agreement, DateTime start, DateTime end, ServiceType typeService)
        {
            Id = id;
            PatientId = patientId;
            DoctorId = doctorId;
            ServiceId = serviceId;
            Agreement = agreement;
            Start = start;
            End = end;
            TypeService = typeService;
        }

        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int ServiceId { get; set; }
        public string Agreement { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public ServiceType TypeService { get; set; }
    }
}
