using ClinicManager.Api.Models.DoctorModels;
using ClinicManager.Application.Models;
using ClinicManager.Core.Repositories;
using ClinicManager.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClinicManager.Application.Queries.Doctors.GetByIdDoctor
{
    public class GetDoctorByIdHandler : IRequestHandler<GetDoctorByIdQuery, ResultViewModel<DoctorViewModel>>
    {
        private readonly IDoctorRepository _repository;

        public GetDoctorByIdHandler(IDoctorRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<DoctorViewModel>> Handle(GetDoctorByIdQuery request, CancellationToken cancellationToken)
        {
            var doctor = await _repository.GetByIdAsync(request.Id);

            if (doctor == null)
            {
                return ResultViewModel<DoctorViewModel>.Error("Médico não encontrado.");
            }

            var doctorViewModel = DoctorViewModel.FromEntity(doctor);

            return ResultViewModel<DoctorViewModel>.Success(doctorViewModel);
        }
    }
}
