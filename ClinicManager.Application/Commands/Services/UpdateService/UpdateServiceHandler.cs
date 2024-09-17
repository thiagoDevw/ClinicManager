using Azure.Core;
using ClinicManager.Application.Models;
using ClinicManager.Infrastructure.Persistence;
using MediatR;

namespace ClinicManager.Application.Commands.CommandsServices.UpdateService
{
    public class UpdateServiceHandler : IRequestHandler<UpdateServiceCommand, ResultViewModel>
    {
        private readonly ClinicDbContext _context;

        public UpdateServiceHandler(ClinicDbContext context)
        {
            _context = context;
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

            var existingService = await _context.Services.FindAsync(new object[] { request.Id }, cancellationToken);
            if (existingService == null)
            {
                return ResultViewModel.Error($"Serviço com ID {request.Id} não encontrado.");
            }

            existingService.Name = request.Name;
            existingService.Description = request.Description;
            existingService.Value = request.Value;
            existingService.Duration = request.Duration;

            _context.Services.Update(existingService);
            await _context.SaveChangesAsync();


            return ResultViewModel.Success();
        }
    }
}
