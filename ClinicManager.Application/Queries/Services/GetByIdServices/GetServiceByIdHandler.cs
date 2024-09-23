using ClinicManager.Api.Models.ServiceModels;
using ClinicManager.Application.Models;
using ClinicManager.Core.Repositories;
using MediatR;

namespace ClinicManager.Application.Queries.Services.GetByIdServices
{
    public class GetServiceByIdHandler : IRequestHandler<GetServiceByIdQuery, ResultViewModel<ServiceViewModel>>
    {
        private readonly IServiceRepository _repository;

        public GetServiceByIdHandler(IServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<ServiceViewModel>> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken)
        {
            var service = await _repository.GetByIdAsync(request.Id);

            if (service == null)
            {
                return ResultViewModel<ServiceViewModel>.Error("Paciente não encontrado.");
            }

            var serviceViewModel = ServiceViewModel.FromEntity(service);

            return ResultViewModel<ServiceViewModel>.Success(serviceViewModel);
        }
    }
}
