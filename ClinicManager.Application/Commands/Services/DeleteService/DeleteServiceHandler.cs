using ClinicManager.Application.Models;
using ClinicManager.Core.Repositories;
using ClinicManager.Infrastructure.Persistence;
using MediatR;

namespace ClinicManager.Application.Commands.CommandsServices.DeleteService
{
    public class DeleteServiceHandler : IRequestHandler<DeleteServiceCommand, ResultViewModel<int>>
    {
        private readonly IServiceRepository _repository;

        public DeleteServiceHandler(IServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<int>> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
        {
            var service = await _repository.GetByIdAsync(request.Id);

            if (service == null)
            {
                return ResultViewModel<int>.Error("Serviço não encontrado.");
            }

            await _repository.DeleteAsync(service);

            return ResultViewModel<int>.Success(service.Id);
        }
    }
}
