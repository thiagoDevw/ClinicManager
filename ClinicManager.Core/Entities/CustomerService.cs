using ClinicManager.Core.Enums;


namespace ClinicManager.Core.Entities
{
    public class CustomerService : BaseEntity
    {
        public CustomerService() { }
        public CustomerService(int patientId, int serviceId, int doctorId, string agreement,  ServiceType typeService)
        {
            PatientId = patientId;
            ServiceId = serviceId;
            DoctorId = doctorId;
            Agreement = agreement;
            TypeService = typeService;
            Start = DateTime.Now;
        }

        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public string Agreement { get; set; }
        public DateTime Start {  get; set; }
        public DateTime End { get; set; }
        public ServiceType TypeService { get; set; }

    }
}
