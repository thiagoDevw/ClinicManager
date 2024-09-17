using ClinicManager.Application.Models;
using ClinicManager.Application.Models.ServiceModels;
using MediatR;

namespace ClinicManager.Application.Queries.Services.GetAllServices
{
    public class GetAllServicesQuery : IRequest<ResultViewModel<List<ServiceItemViewModel>>>
    {
        public GetAllServicesQuery(string query)
        {
            Query = query;
        }

        public string Query { get; set; }
    }
}
