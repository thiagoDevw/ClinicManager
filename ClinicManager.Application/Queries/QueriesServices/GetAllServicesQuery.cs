using ClinicManager.Application.Models;
using ClinicManager.Application.Models.ServiceModels;
using ClinicManager.Infrastructure.Persistence;
using MediatR;

namespace ClinicManager.Application.Queries.QueriesServices
{
    public class GetAllServicesQuery : IRequest<ResultViewModel<List<ServiceItemViewModel>>>
    {
        public GetAllServicesQuery(string query)
        {
            Query = query;
        }

        public string Query { get; set; }
    }

    public class GetAllServiceHandler : IRequestHandler<GetAllServicesQuery, ResultViewModel<List<ServiceItemViewModel>>>
    {
        private readonly ClinicDbContext _context;

        public GetAllServiceHandler(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<List<ServiceItemViewModel>>> Handle(GetAllServicesQuery request, CancellationToken cancellationToken)
        {
            var query = request.Query?.ToLower();

            var services = _context.Services
                .Where(s => string.IsNullOrEmpty(query) ||
                s.Name.ToLower().Contains(query) ||
                   s.Description.ToLower().Contains(query))
                .Select(s => ServiceItemViewModel.FromEntity(s))
                .ToList();

            return ResultViewModel<List<ServiceItemViewModel>>.Success(services);
        }
    }
}
