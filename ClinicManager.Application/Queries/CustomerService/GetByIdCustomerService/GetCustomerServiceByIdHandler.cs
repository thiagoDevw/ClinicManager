using ClinicManager.Api.Models.CustomerModels;
using ClinicManager.Application.Models;
using ClinicManager.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClinicManager.Application.Queries.CustomerService.GetByIdCustomerService
{
    public class GetCustomerServiceByIdHandler : IRequestHandler<GetCustomerServiceByIdQuery, ResultViewModel<CustomerViewModel>>
    {
        private readonly ClinicDbContext _context;

        public GetCustomerServiceByIdHandler(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<CustomerViewModel>> Handle(GetCustomerServiceByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _context.CustomerServices
                .Where(c => c.Id == request.Id)
                .Select(c => new CustomerViewModel
                {
                    Id = c.Id,
                    PatientName = c.Patient.Name,
                    DoctorName = c.Doctor.Name,
                    ServiceName = c.Service.Name,
                    Agreement = c.Agreement,
                    Start = c.Start,
                    End = c.End,
                    TypeService = c.TypeService
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (customer == null)
            {
                return ResultViewModel<CustomerViewModel>.Error("Atendimento não encontrado.");
            }

            return ResultViewModel<CustomerViewModel>.Success(customer);
        }
    }
}
