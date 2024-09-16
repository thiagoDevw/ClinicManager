using ClinicManager.Application.Models;
using ClinicManager.Infrastructure.Persistence;
using MediatR;

namespace ClinicManager.Application.Commands.CommandsServices.DeleteService
{
    public class DeleteServiceHandler : IRequestHandler<DeleteServiceCommand, ResultViewModel<int>>
    {
        private readonly ClinicDbContext _context;

        public DeleteServiceHandler(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<int>> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
        {
            var service = await _context.Services.FindAsync(request.Id);

            if (service == null)
            {
                return ResultViewModel<int>.Error("Serviço não encontrado.");
            }

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return ResultViewModel<int>.Success(service.Id);
        }
    }
}
