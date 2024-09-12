using ClinicManager.Application.Models;
using MediatR;

namespace ClinicManager.Application.Commands.CommandsCustomerService.DeleteCustomerService
{
    public class DeleteCustomerCommand : IRequest<ResultViewModel>
    {
        public int Id { get; set; }

        public DeleteCustomerCommand(int id)
        {
            Id = id;
        }
    }
}
