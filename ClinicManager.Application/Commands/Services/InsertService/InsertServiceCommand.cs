using ClinicManager.Application.Models;
using MediatR;

namespace ClinicManager.Application.Commands.CommandsServices.InsertService
{
    public class InsertServiceCommand : IRequest<ResultViewModel<int>>
    {
        public InsertServiceCommand(string name, string description, decimal value, int duration)
        {
            Name = name;
            Description = description;
            Value = value;
            Duration = duration;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public int Duration { get; set; }
    }
}
