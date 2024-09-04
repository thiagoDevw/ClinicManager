namespace ClinicManager.Api.Models.DoctorModels
{
    public class CreateDoctorInputModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone {  get; set; }
        public string Email { get; set; }
        public string CPF {  get; set; }
        public string BloodType { get; set; }
        public string Address { get; set; }
        public string Specialty { get; set; }
        public string CRM { get; set; }
    }
}
