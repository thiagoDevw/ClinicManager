using System.Text.Json.Serialization;

namespace ClinicManager.Core.Entities
{
    public class Patient : BaseEntity
    {
        public Patient() { }
        public Patient(string name, string lastName, string cpf) 
        {
            Name = name;
            LastName = lastName;
            CPF = cpf;
        }

        private string _cpf; 

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

        public ICollection<CustomerService> CustomerServices { get; set; } = new List<CustomerService>();

        
    }
}
