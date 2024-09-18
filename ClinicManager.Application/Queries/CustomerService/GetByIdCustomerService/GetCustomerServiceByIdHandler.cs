using ClinicManager.Api.Models.CustomerModels;
using ClinicManager.Application.Models;
using ClinicManager.Core.Entities;
using ClinicManager.Core.Repositories;
using ClinicManager.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClinicManager.Application.Queries.CustomerService.GetByIdCustomerService
{
    public class GetCustomerServiceByIdHandler : IRequestHandler<GetCustomerServiceByIdQuery, ResultViewModel<CustomerViewModel>>
    {
        private readonly ICustomerServiceRepository _repository;

        public GetCustomerServiceByIdHandler(ICustomerServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<CustomerViewModel>> Handle(GetCustomerServiceByIdQuery request, CancellationToken cancellationToken)
        {
            var customerService = await _repository.GetByIdAsync(request.Id);

            if (customerService == null)
            {
                return ResultViewModel<CustomerViewModel>.Error("Atendimento não encontrado.");
            }

            var customerViewModel = new CustomerViewModel
            {
                Id = customerService.Id,
                PatientName = customerService.Patient.Name,
                DoctorName = customerService.Doctor.Name,
                ServiceName = customerService.Service.Name,
                Agreement = customerService.Agreement,
                Start = customerService.Start,
                End = customerService.End,
                TypeService = customerService.TypeService
            };

            return ResultViewModel<CustomerViewModel>.Success(customerViewModel);
        }
    }
}
