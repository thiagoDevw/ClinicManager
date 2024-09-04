using ClinicManager.Core.Entities;

namespace ClinicManager.Application.Models.DoctorModels
{
    public class DoctorItemViewModel
    {
        public DoctorItemViewModel(int id, string name, string lastName, string email, string cPF, string cRM)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            Email = email;
            CPF = cPF;
            CRM = cRM;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string CRM { get; set; }

        public static DoctorItemViewModel FromEntity(Doctor entity)
        {
            return new DoctorItemViewModel(
                entity.Id,
                entity.Name,
                entity.LastName,
                entity.Email,
                entity.CPF,
                entity.CRM
                );
        }
    }
}
