using ClinicManager.Application.Models;
using ClinicManager.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.CommandsCustomerService.DeleteCustomerService
{
    internal class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, ResultViewModel>
    {
        private readonly ClinicDbContext _context;

        public DeleteCustomerHandler(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customerService = await _context.CustomerServices
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (customerService == null)
            {
                return ResultViewModel.Error("Atendimento não encontrado.");
            }
            _context.CustomerServices.Remove(customerService);
            await _context.SaveChangesAsync(cancellationToken);

            return ResultViewModel.Success();
        }
    }
}
