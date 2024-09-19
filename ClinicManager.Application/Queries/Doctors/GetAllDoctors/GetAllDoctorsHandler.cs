using ClinicManager.Application.Models;
using ClinicManager.Application.Models.DoctorModels;
using ClinicManager.Core.Repositories;
using MediatR;

namespace ClinicManager.Application.Queries.Doctors.GetAllDoctors
{
    public class GetAllDoctorsHandler : IRequestHandler<GetAllDoctorsQuery, ResultViewModel<List<DoctorItemViewModel>>>
    {
        private readonly IDoctorRepository _repository;

        public GetAllDoctorsHandler(IDoctorRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<List<DoctorItemViewModel>>> Handle(GetAllDoctorsQuery request, CancellationToken cancellationToken)
        {
            var doctorsQuery = await _repository.GetAllAsync(request.Query);

            var doctorViewModels = doctorsQuery
                .Select(d => DoctorItemViewModel.FromEntity(d))
                .ToList();

            return ResultViewModel<List<DoctorItemViewModel>>.Success(doctorViewModels);
        }
    }
}
