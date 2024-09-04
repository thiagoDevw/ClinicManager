using ClinicManager.Core.Entities;

namespace ClinicManager.Api.Models.PatientsModels
{
    public class PatientViewModel
    {
        public PatientViewModel() { }
        public PatientViewModel(int id, string name, string lastName, DateTime dateOfBirth, string phone, string email, string cPF, string bloodType, double height, double weight, string address)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Phone = phone;
            Email = email;
            CPF = cPF;
            BloodType = bloodType;
            Height = height;
            Weight = weight;
            Address = address;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone {  get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string BloodType { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string Address { get; set; }

        public static PatientViewModel FromEntity(Patient entity)
        {
            return new PatientViewModel(
                entity.Id,
                entity.Name,
                entity.LastName,
                entity.DateOfBirth,
                entity.Phone,
                entity.Email,
                entity.CPF,
                entity.BloodType,
                entity.Height,
                entity.Weight,
                entity.Address
                );
        }
    }
}
