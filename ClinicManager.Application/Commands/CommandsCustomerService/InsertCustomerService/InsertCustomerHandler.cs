using ClinicManager.Application.Commands.CommandsCustomerService.CreateCustomerService;
using ClinicManager.Application.Models;
using ClinicManager.Core.Entities;
using ClinicManager.Infrastructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.CommandsCustomerService.InsertCustomerService
{
    internal class InsertCustomerHandler : IRequestHandler<InsertCustomerServiceCommand, ResultViewModel<int>>
    {
        private readonly ClinicDbContext _context;

        public InsertCustomerHandler(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<int>> Handle(InsertCustomerServiceCommand request, CancellationToken cancellationToken)
        {
            var customerSevice = new CustomerService
            {
                PatientId = request.PatientId,
                ServiceId = request.ServiceId,
                DoctorId = request.DoctorId,
                Agreement = request.Agreement,
                TypeService = request.TypeService,
                Start = request.Start,
                End = request.End
            };

            _context.CustomerServices.Add(customerSevice);
            await _context.SaveChangesAsync(cancellationToken);

            return new ResultViewModel<int>(customerSevice.Id);
        }
    }
}
