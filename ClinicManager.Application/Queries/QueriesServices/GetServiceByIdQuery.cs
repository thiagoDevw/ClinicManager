using ClinicManager.Api.Models.ServiceModels;
using ClinicManager.Application.Models;
using MediatR;

namespace ClinicManager.Application.Queries.QueriesServices
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
