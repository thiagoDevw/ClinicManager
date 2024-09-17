using ClinicManager.Application.Models;
using MediatR;

namespace ClinicManager.Application.Commands.CommandsServices.UpdateService
{
    public class UpdateServiceCommand : IRequest<ResultViewModel>
    {
        public UpdateServiceCommand(int id, string name, string description, decimal value, int duration)
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
    }
}
