using ClinicManager.Api.Models.PatientsModels;
using ClinicManager.Application.Models;
using MediatR;

namespace ClinicManager.Application.Queries.Patients.GetByIdPatients
{
    public class GetPatientsByIdQuery : IRequest<ResultViewModel<PatientViewModel>>
    {
        public GetPatientsByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }

    }
}
