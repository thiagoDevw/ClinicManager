using ClinicManager.Api.Models.DoctorModels;
using ClinicManager.Application.Models;
using ClinicManager.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClinicManager.Application.Queries.QueriesDoctors
{
    public class GetDoctorByIdHandler : IRequestHandler<GetDoctorByIdQuery, ResultViewModel<DoctorViewModel>>
    {
        private readonly ClinicDbContext _context;

        public GetDoctorByIdHandler(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<DoctorViewModel>> Handle(GetDoctorByIdQuery request, CancellationToken cancellationToken)
        {
            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken);

            if (doctor == null)
            {
                return ResultViewModel<DoctorViewModel>.Error("Médico não encontrado.");
            }

            var doctorViewModel = DoctorViewModel.FromEntity(doctor);

            return ResultViewModel<DoctorViewModel>.Success(doctorViewModel);
        }
    }
}
