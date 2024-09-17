using ClinicManager.Api.Models.ServiceModels;
using ClinicManager.Application.Models;
using ClinicManager.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClinicManager.Application.Queries.Services.GetByIdServices
{
    public class GetServiceByIdHandler : IRequestHandler<GetServiceByIdQuery, ResultViewModel<ServiceViewModel>>
    {
        private readonly ClinicDbContext _context;

        public GetServiceByIdHandler(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<ServiceViewModel>> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken)
        {
            var service = await _context.Services
               .FirstOrDefaultAsync(s => s.Id == request.Id);

            if (service == null)
            {
                return ResultViewModel<ServiceViewModel>.Error("Paciente não encontrado.");
            }

            var serviceViewModel = ServiceViewModel.FromEntity(service);

            return ResultViewModel<ServiceViewModel>.Success(serviceViewModel);
        }
    }
}
