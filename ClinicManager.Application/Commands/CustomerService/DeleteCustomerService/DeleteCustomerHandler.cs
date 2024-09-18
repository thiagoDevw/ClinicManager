using ClinicManager.Application.Models;
using ClinicManager.Core.Repositories;
using ClinicManager.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClinicManager.Application.Commands.CommandsCustomerService.DeleteCustomerService
{
    internal class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, ResultViewModel>
    {
        private readonly ICustomerServiceRepository _repository;

        public DeleteCustomerHandler(ICustomerServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customerService = await _repository.GetByIdAsync(request.Id);

            if (customerService == null)
            {
                return ResultViewModel.Error("Atendimento não encontrado.");
            }

            await _repository.DeleteAsync(request.Id);
            return ResultViewModel.Success();
        }
    }
}
