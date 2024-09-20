using Azure.Core;
using ClinicManager.Application.Models;
using ClinicManager.Core.Repositories;
using ClinicManager.Infrastructure.Persistence;
using MediatR;

namespace ClinicManager.Application.Commands.CommandsPatients.UpdatePatients
{
    public class UpdatePatientHandler : IRequestHandler<UpdatePatientCommand, ResultViewModel>
    {
        private readonly IPatientRepository _repository;

        public UpdatePatientHandler(IPatientRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return ResultViewModel<int>.Error("Dados do paciente não fornecidos.");
            }

            var existingPatient = await _repository.GetByIdAsync(request.Id);

            if (existingPatient == null)
            {
                return ResultViewModel<int>.Error($"Paciente com ID {request.Id} não encontrado.");
            }

            existingPatient.Name = request.Name;
            existingPatient.LastName = request.LastName;
            existingPatient.DateOfBirth = request.DateOfBirth;
            existingPatient.Phone = request.Phone;
            existingPatient.Email = request.Email;
            existingPatient.CPF = request.CPF;
            existingPatient.BloodType = request.BloodType;
            existingPatient.Height = request.Height;
            existingPatient.Weight = request.Weight;
            existingPatient.Address = request.Address;

            await _repository.UpdateAsync(existingPatient);

            return ResultViewModel.Success();
        }
    }
}
