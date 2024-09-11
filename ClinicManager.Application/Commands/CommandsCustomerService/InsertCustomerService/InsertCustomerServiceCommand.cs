using ClinicManager.Application.Models;
using ClinicManager.Core.Enums;
using MediatR;

namespace ClinicManager.Application.Commands.CommandsCustomerService.CreateCustomerService
{
    public class InsertCustomerServiceCommand : IRequest<ResultViewModel<int>>
    {
        public int PatientId { get; set; }
        public int ServiceId { get; set; }
        public int DoctorId { get; set; }
        public string Agreement { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public ServiceType TypeService { get; set; }
    }
}
