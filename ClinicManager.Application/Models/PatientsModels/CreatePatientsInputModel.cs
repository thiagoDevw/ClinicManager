using System.Text.Json.Serialization;
using System.Text.Json;

namespace ClinicManager.Api.Models.PatientsModels
{
    public class CreatePatientsInputModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone {  get; set; }
        public string Email { get; set; }
        public string CPF {  get; set; }
        public string BloodType { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string Address { get; set; }
    }
}
