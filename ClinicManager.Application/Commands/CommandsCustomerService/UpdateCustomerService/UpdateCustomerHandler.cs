using ClinicManager.Application.Models;
using ClinicManager.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClinicManager.Application.Commands.CommandsCustomerService.UpdateCustomerService
{
    public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, ResultViewModel>
    {
        private readonly ClinicDbContext _context;

        public UpdateCustomerHandler(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customerService = await _context.CustomerServices
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (customerService == null)
            {
                return ResultViewModel.Error("Atendimento não encontrado.");
            }

            customerService.PatientId = request.PatientId;
            customerService.DoctorId = request.DoctorId;
            customerService.ServiceId = request.ServiceId;
            customerService.Agreement = request.Agreement;
            customerService.Start = request.Start;
            customerService.End = request.End;
            customerService.TypeService = request.TypeService;

            await _context.SaveChangesAsync(cancellationToken);

            return ResultViewModel.Success();
        }
    }
}
