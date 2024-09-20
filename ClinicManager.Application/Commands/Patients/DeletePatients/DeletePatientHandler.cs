using ClinicManager.Application.Models;
using ClinicManager.Core.Repositories;
using ClinicManager.Infrastructure.Persistence;
using MediatR;

namespace ClinicManager.Application.Commands.CommandsPatients.DeletePatients
{
    public class DeletePatientHandler : IRequestHandler<DeletePatientCommand, ResultViewModel<int>>
    {
        private readonly IPatientRepository _repository;

        public DeletePatientHandler(IPatientRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<int>> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
        {
            var patient = await _repository.GetByIdAsync(request.Id);

            if (patient == null)
            {
                return ResultViewModel<int>.Error("Paciente não encontrado.");
            }

            await _repository.DeleteAsync(patient);

            return ResultViewModel<int>.Success(patient.Id);
        }
    }
}
