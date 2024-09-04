using ClinicManager.Core.Entities;
using ClinicManager.Core.Enums;

namespace ClinicManager.Api.Models.CustomerModels
{
    public class CustomerViewModel : BaseEntity
    {
        public CustomerViewModel() { }
        public CustomerViewModel(int id, string patientName, string doctorName, string serviceName, string agreement, DateTime start, DateTime end, ServiceType typeService)
        {
            Id = id;
            PatientName = patientName;
            DoctorName = doctorName;
            ServiceName = serviceName;
            Agreement = agreement;
            Start = start;
            End = end;
            TypeService = typeService;
        }

        public int Id { get; set; }
        public string PatientName {  get; set; }
        public string DoctorName { get; set; }
        public string ServiceName { get; set; }
        public string Agreement { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public ServiceType TypeService { get; set; }

        public static CustomerViewModel FromEntity(CustomerService entity, string patientName, string doctorName, string serviceName)
        {
            return new CustomerViewModel(
                entity.Id,
                patientName,
                doctorName,
                serviceName,
                entity.Agreement,
                entity.Start,
                entity.End,
                entity.TypeService
                );
        }
    }
}
