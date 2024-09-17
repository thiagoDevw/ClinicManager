using ClinicManager.Api.Models.ServiceModels;
using ClinicManager.Application.Models;
using MediatR;

namespace ClinicManager.Application.Queries.Services.GetByIdServices
{
    public class GetServiceByIdQuery : IRequest<ResultViewModel<ServiceViewModel>>
    {
        public GetServiceByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
