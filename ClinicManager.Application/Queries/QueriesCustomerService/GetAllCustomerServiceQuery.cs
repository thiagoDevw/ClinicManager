using ClinicManager.Application.Models;
using ClinicManager.Application.Models.CustomerModels;
using EllipticCurve;
using MediatR;

namespace ClinicManager.Application.Queries.QueriesCustomerService
{
    public class GetAllCustomerServiceQuery : IRequest<ResultViewModel<List<CustomerItemViewModel>>>
    {
        public GetAllCustomerServiceQuery(string search)
        {
            Search = search;
        }

        public string Search { get; set; }
    }
}
