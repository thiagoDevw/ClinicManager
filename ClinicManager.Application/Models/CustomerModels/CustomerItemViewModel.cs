using ClinicManager.Core.Entities;

namespace ClinicManager.Application.Models.CustomerModels
{
    public class CustomerItemViewModel
    {
        public CustomerItemViewModel(int id, string patientName, string doctorName, string serviceName, string agreement)
        {
            Id = id;
            PatientName = patientName;
            DoctorName = doctorName;
            ServiceName = serviceName;
            Agreement = agreement;
        }

        public int Id { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public string ServiceName { get; set; }
        public string Agreement { get; set; }

        public static CustomerItemViewModel FromEntity(CustomerService entity, string patientName, string doctorName, string serviceName)
        {
            return new CustomerItemViewModel(
                entity.Id,
                patientName,
                doctorName,
                serviceName,
                entity.Agreement
                );
        }
    }
}
