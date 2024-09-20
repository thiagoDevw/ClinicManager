using ClinicManager.Application.Models;
using ClinicManager.Application.Models.ServiceModels;
using ClinicManager.Core.Repositories;
using MediatR;

namespace ClinicManager.Application.Queries.Services.GetAllServices
{
    public class GetAllServiceHandler : IRequestHandler<GetAllServicesQuery, ResultViewModel<List<ServiceItemViewModel>>>
    {
        private readonly IServiceRepository _repository;

        public GetAllServiceHandler(IServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<List<ServiceItemViewModel>>> Handle(GetAllServicesQuery request, CancellationToken cancellationToken)
        {
            var services = await _repository.GetAllAsync(request.Query);

            var serviceViewModels = services.Select(s => ServiceItemViewModel.FromEntity(s)).ToList();

            return ResultViewModel<List<ServiceItemViewModel>>.Success(serviceViewModels);
        }
    }
}
