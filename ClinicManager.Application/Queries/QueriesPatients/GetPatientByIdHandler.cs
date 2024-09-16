using ClinicManager.Api.Models.PatientsModels;
using ClinicManager.Application.Models;
using ClinicManager.Infrastructure.Persistence;
using MediatR;

namespace ClinicManager.Application.Queries.QueriesPatients
{
    public class GetPatientByIdHandler : IRequestHandler<GetPatientsByIdQuery, ResultViewModel<PatientViewModel>>
    {
        private readonly ClinicDbContext _context;

        public GetPatientByIdHandler(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<PatientViewModel>> Handle(GetPatientsByIdQuery request, CancellationToken cancellationToken)
        {
            var patient = _context.Patients
                .FirstOrDefault(p => p.Id == request.Id);

            if (patient == null)
            {
                return ResultViewModel<PatientViewModel>.Error("Paciente não encontrado.");
            }

            var patientViewModel = PatientViewModel.FromEntity(patient);

            return ResultViewModel<PatientViewModel>.Success(patientViewModel);
        }
    }
}
