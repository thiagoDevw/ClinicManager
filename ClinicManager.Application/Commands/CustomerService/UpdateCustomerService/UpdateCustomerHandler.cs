using ClinicManager.Application.Models;
using ClinicManager.Core.Repositories;
using ClinicManager.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClinicManager.Application.Commands.CommandsCustomerService.UpdateCustomerService
{
    public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, ResultViewModel>
    {
        private readonly ICustomerServiceRepository _repository;

        public UpdateCustomerHandler(ICustomerServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customerService = await _repository.GetByIdAsync(request.Id);
            if (customerService == null)
            {
                return ResultViewModel.Error("Atendimento não encontrado.");
            }

            customerService.PatientId = request.PatientId;
            customerService.DoctorId = request.DoctorId;
            customerService.ServiceId = request.ServiceId;
            customerService.Agreement = request.Agreement;
            customerService.Start = request.Start;
            customerService.End = request.End;
            customerService.TypeService = request.TypeService;

            await _repository.UpdateAsync(customerService);

            return ResultViewModel.Success();
        }
    }
}
