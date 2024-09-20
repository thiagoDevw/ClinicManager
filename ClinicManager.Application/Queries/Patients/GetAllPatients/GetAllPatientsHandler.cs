using ClinicManager.Application.Models;
using ClinicManager.Application.Models.PatientsModels;
using ClinicManager.Core.Repositories;
using ClinicManager.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClinicManager.Application.Queries.Patients.GetAllPatients
{
    public class GetAllPatientsHandler : IRequestHandler<GetAllPatientsQuery, ResultViewModel<List<PatientItemViewModel>>>
    {
        private readonly IPatientRepository _repository;

        public GetAllPatientsHandler(IPatientRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<List<PatientItemViewModel>>> Handle(GetAllPatientsQuery request, CancellationToken cancellationToken)
        {
            var patients = await _repository.GetAllAsync(request.Query);



            var result = patients.Select(p => PatientItemViewModel.FromEntity(p)).ToList();

            return ResultViewModel<List<PatientItemViewModel>>.Success(result);
        }
    }
}
