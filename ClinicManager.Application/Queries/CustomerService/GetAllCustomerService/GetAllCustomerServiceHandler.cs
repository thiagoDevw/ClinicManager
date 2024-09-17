using ClinicManager.Application.Models;
using ClinicManager.Application.Models.CustomerModels;
using ClinicManager.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClinicManager.Application.Queries.CustomerService.GetAllCustomerService
{
    public class GetAllCustomerServiceHandler : IRequestHandler<GetAllCustomerServiceQuery, ResultViewModel<List<CustomerItemViewModel>>>
    {
        private readonly ClinicDbContext _context;

        public GetAllCustomerServiceHandler(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<List<CustomerItemViewModel>>> Handle(GetAllCustomerServiceQuery request, CancellationToken cancellationToken)
        {

            var query = _context.CustomerServices
                    .Include(cs => cs.Patient)
                    .Include(cs => cs.Doctor)
                    .Include(cs => cs.Service)
                    .AsQueryable();

            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(c => c.Patient.Name.ToLower().Contains(request.Search.ToLower()) ||
                                         c.Doctor.Name.ToLower().Contains(request.Search.ToLower()) ||
                                         c.Service.Name.ToLower().Contains(request.Search.ToLower()));
            }

            // Logging para verificar os resultados filtrados
            var filteredCount = await query.CountAsync(cancellationToken);
            Console.WriteLine($"Número de registros após o filtro: {filteredCount}");

            var totalRecords = await _context.CustomerServices.CountAsync(cancellationToken);

            var customers = await query
                .OrderBy(c => c.Patient.Name)
                /*.Skip((page - 1) * pageSize)
                .Take(pageSize)*/
                .Select(c => new CustomerItemViewModel(
                    c.Id,
                    c.Patient.Name,
                    c.Doctor.Name,
                    c.Service.Name,
                    c.Agreement
                ))
                .ToListAsync(cancellationToken);

            var result = new
            {
                TotalRecords = totalRecords,
                Data = customers
            };

            return ResultViewModel<List<CustomerItemViewModel>>.Success(customers);
        }
    }
}
