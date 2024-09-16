using ClinicManager.Application.Models;
using ClinicManager.Application.Models.DoctorModels;
using ClinicManager.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClinicManager.Application.Queries.QueriesDoctors
{
    public class GetAllDoctorsHandler : IRequestHandler<GetAllDoctorsQuery, ResultViewModel<List<DoctorItemViewModel>>>
    {
        private readonly ClinicDbContext _context;

        public GetAllDoctorsHandler(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<List<DoctorItemViewModel>>> Handle(GetAllDoctorsQuery request, CancellationToken cancellationToken)
        {
            var doctorsQuery = _context.Doctors.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Query))
            {
                var query = request.Query .ToLower();
                doctorsQuery = doctorsQuery.Where(d =>
                d.Name.ToLower().Contains(query) ||
                d.LastName.ToLower().Contains(query) ||
                d.Email.ToLower().Contains(query) ||
                d.CPF.ToLower().Contains(query) ||
                    d.CRM.ToLower().Contains(query));
            }

            var doctors = await doctorsQuery
                .Select(d => DoctorItemViewModel.FromEntity(d))
                .ToListAsync(cancellationToken);

            return ResultViewModel<List<DoctorItemViewModel>>.Success(doctors);
        }
    }
}
