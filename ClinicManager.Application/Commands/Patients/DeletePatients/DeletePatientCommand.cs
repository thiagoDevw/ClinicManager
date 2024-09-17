using ClinicManager.Application.Models;
using MediatR;

namespace ClinicManager.Application.Commands.CommandsPatients.DeletePatients
{
    public class DeletePatientCommand : IRequest<ResultViewModel<int>>
    {
        public DeletePatientCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }

    }
}
