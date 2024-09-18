using ClinicManager.Application.Models;
using ClinicManager.Application.Models.CustomerModels;
using ClinicManager.Core.Repositories;
using MediatR;

namespace ClinicManager.Application.Queries.CustomerService.GetAllCustomerService
{
    public class GetAllCustomerServiceHandler : IRequestHandler<GetAllCustomerServiceQuery, ResultViewModel<List<CustomerItemViewModel>>>
    {
        private readonly ICustomerServiceRepository _repository;

        public GetAllCustomerServiceHandler(ICustomerServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<List<CustomerItemViewModel>>> Handle(GetAllCustomerServiceQuery request, CancellationToken cancellationToken)
        {
            var (totalRecords, customerServices) = await _repository.GetAllAsync(
                search: request.Search,
                page: request.Page,
                pageSize: request.PageSize
            );
            var customers = customerServices.Select(c => new CustomerItemViewModel(
                c.Id,
                c.Patient.Name,
                c.Doctor.Name,
                c.Service.Name,
                c.Agreement
            )).ToList();

            return ResultViewModel<List<CustomerItemViewModel>>.Success(customers);
        }
    }
}
