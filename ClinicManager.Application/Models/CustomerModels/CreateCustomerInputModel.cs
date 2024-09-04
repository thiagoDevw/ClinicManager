using ClinicManager.Core.Enums;

namespace ClinicManager.Api.Models.CustomerModels
{
    public class CreateCustomerInputModel
    {

        public int PatientId { get; set; } // Id do paciente
        public int ServiceId { get; set; } // Id do serviço
        public int DoctorId { get; set; } // Id do médico
        public string Agreement {  get; set; } // Convênio
        public DateTime Start {  get; set; } // Data e hora de início
        public DateTime End {  get; set; } // Data e hora de início
        public ServiceType TypeService { get; set; } // Tipo de serviço (enum)

    }
}
