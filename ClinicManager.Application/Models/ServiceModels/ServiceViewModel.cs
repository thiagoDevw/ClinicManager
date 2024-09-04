using ClinicManager.Core.Entities;

namespace ClinicManager.Api.Models.ServiceModels
{
    public class ServiceViewModel
    {
        public ServiceViewModel() { }
        public ServiceViewModel(int id, string name, string description, decimal value, int duration)
        {
            Id = id;
            Name = name;
            Description = description;
            Value = value;
            Duration = duration;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public int Duration { get; set; }

        public static ServiceViewModel FromEntity(Service entity)
        {
            return new ServiceViewModel(
                entity.Id,
                entity.Name,
                entity.Description,
                entity.Value,
                entity.Duration
                );
        }
    }
}
