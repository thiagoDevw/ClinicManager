using Azure.Core;
using ClinicManager.Application.Models;
using ClinicManager.Core.Repositories;
using ClinicManager.Infrastructure.Persistence;
using MediatR;

namespace ClinicManager.Application.Commands.CommandsServices.UpdateService
{
    public class UpdateServiceHandler : IRequestHandler<UpdateServiceCommand, ResultViewModel>
    {
        private readonly IServiceRepository _repository;

        public UpdateServiceHandler(IServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return ResultViewModel.Error("Dados do serviço não fornecidos.");
            }

            if (request.Id <= 0)
            {
                return ResultViewModel.Error("Id do serviço não corresponde ao ID fornecido no modelo.");
            }

            var existingService = await _repository.GetByIdAsync(request.Id);
            if (existingService == null)
            {
                return ResultViewModel.Error($"Serviço com ID {request.Id} não encontrado.");
            }

            existingService.Name = request.Name;
            existingService.Description = request.Description;
            existingService.Value = request.Value;
            existingService.Duration = request.Duration;

            await _repository.UpdateAsync(existingService);

            return ResultViewModel.Success();
        }
    }
}
