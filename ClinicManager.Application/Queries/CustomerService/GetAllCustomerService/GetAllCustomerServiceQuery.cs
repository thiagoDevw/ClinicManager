using ClinicManager.Application.Models;
using ClinicManager.Application.Models.CustomerModels;
using EllipticCurve;
using MediatR;

namespace ClinicManager.Application.Queries.CustomerService.GetAllCustomerService
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
