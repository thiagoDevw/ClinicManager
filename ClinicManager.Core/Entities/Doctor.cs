
namespace ClinicManager.Core.Entities
{
    public class Doctor : BaseEntity
    {
        public Doctor() { }
        public Doctor(string name, string lastName, string cpf, string crm, string email)
        {
            Name = name;
            LastName = lastName;
            Email = email;
            CPF = cpf;
            CRM = crm;
            
        }

        public Doctor(int id, string name, string lastName, DateTime dateOfBirth, string phone, string email, string cPF, string bloodType, string address, string specialty, string cRM)
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
            CRM = cRM;
        }

        private string _cpf;

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

        public string FullName => $"{Name} {LastName}";

        public ICollection<CustomerService> CustomerServices { get; set; }
    }
}
