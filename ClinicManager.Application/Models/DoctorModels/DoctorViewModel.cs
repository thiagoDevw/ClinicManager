using ClinicManager.Core.Entities;
using ClinicManager.Api.Models.CustomerModels;

namespace ClinicManager.Api.Models.DoctorModels
{
    public class DoctorViewModel
    {
        public DoctorViewModel() { }
        public DoctorViewModel(int id, string name, string lastName, DateTime dateOfBirth, string phone, string email, string cPF, string bloodType, string address, string specialty, string crm)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Phone = phone;
            Email = email;
            CPF = cPF;
            BloodType = bloodType;
            Address = address;
            Specialty = specialty;
            CRM = crm;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string BloodType { get; set; }
        public string Address { get; set; }
        public string Specialty { get; set; }
        public string CRM { get; set; }

        public static DoctorViewModel FromEntity(Doctor entity)
        {
            return new DoctorViewModel(
                entity.Id,
                entity.Name,
                entity.LastName,
                entity.DateOfBirth,
                entity.Phone,
                entity.Email,
                entity.CPF,
                entity.BloodType,
                entity.Address,
                entity.Specialty,
                entity.CRM
                );
        }
    }
}
