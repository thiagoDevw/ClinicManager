using ClinicManager.Application.Models;
using ClinicManager.Application.Models.CustomerModels;
using EllipticCurve;
using MediatR;

namespace ClinicManager.Application.Queries.CustomerService.GetAllCustomerService
{
    public class GetAllCustomerServiceQuery : IRequest<ResultViewModel<List<CustomerItemViewModel>>>
    {
        public GetAllCustomerServiceQuery(string search, int? page, int? pageSize)
        {
            Search = search;
            Page = page;
            PageSize = pageSize;
        }

        public string Search { get; set; }
        public int? Page { get; set; } // Para paginação
        public int? PageSize { get; set; } // Para paginação
    }
}
