
namespace ClinicManager.Api.Models.ServiceModels
{
    public class CreateServiceInputModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public int Duration { get; set; }
    }
}
