using ClinicManager.Core.Entities;

namespace ClinicManager.Application.Models.PatientsModels
{
    public class PatientItemViewModel
    {
        public PatientItemViewModel(int id, string name, string lastName, string email, string cPF)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            Email = email;
            CPF = cPF;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }

        public static PatientItemViewModel FromEntity(Patient entity)
        {
            return new PatientItemViewModel(
                entity.Id,
                entity.Name,
                entity.LastName,
                entity.Email,
                entity.CPF
                );
        }

    }
}
