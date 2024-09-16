using ClinicManager.Application.Models;
using MediatR;

namespace ClinicManager.Application.Commands.CommandsServices.DeleteService
{
    public class DeleteServiceCommand : IRequest<ResultViewModel<int>>
    {
        public DeleteServiceCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
