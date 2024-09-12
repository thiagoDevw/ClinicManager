using ClinicManager.Application.Models;
using ClinicManager.Infrastructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


            throw new NotImplementedException();
        }
    }
}
