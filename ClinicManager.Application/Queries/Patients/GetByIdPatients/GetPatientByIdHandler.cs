using ClinicManager.Api.Models.PatientsModels;
using ClinicManager.Application.Models;
using ClinicManager.Core.Repositories;
using ClinicManager.Infrastructure.Persistence;
using MediatR;

namespace ClinicManager.Application.Queries.Patients.GetByIdPatients
{
    public class GetPatientByIdHandler : IRequestHandler<GetPatientsByIdQuery, ResultViewModel<PatientViewModel>>
    {
        private readonly IPatientRepository _repository;

        public GetPatientByIdHandler(IPatientRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<PatientViewModel>> Handle(GetPatientsByIdQuery request, CancellationToken cancellationToken)
        {
            var patient = await _repository.GetByIdAsync(request.Id);

            if (patient == null)
            {
                return ResultViewModel<PatientViewModel>.Error("Paciente não encontrado.");
            }

            var patientViewModel = PatientViewModel.FromEntity(patient);

            return ResultViewModel<PatientViewModel>.Success(patientViewModel);
        }
    }
}
