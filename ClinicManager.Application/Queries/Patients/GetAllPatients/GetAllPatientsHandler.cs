using ClinicManager.Application.Models;
using ClinicManager.Application.Models.PatientsModels;
using ClinicManager.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClinicManager.Application.Queries.Patients.GetAllPatients
{
    public class GetAllPatientsHandler : IRequestHandler<GetAllPatientsQuery, ResultViewModel<List<PatientItemViewModel>>>
    {
        private readonly ClinicDbContext _context;

        public GetAllPatientsHandler(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<List<PatientItemViewModel>>> Handle(GetAllPatientsQuery request, CancellationToken cancellationToken)
        {
            var patients = _context.Patients.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Query))
            {
                var query = request.Query.ToLower();
                patients = patients.Where(p =>
                p.Name.ToLower().Contains(query) ||
                p.LastName.ToLower().Contains(query) ||
                p.Email.ToLower().Contains(query) ||
                    p.CPF.ToLower().Contains(query));
            }

            var result = await patients
                .Select(p => PatientItemViewModel.FromEntity(p))
                .ToListAsync(cancellationToken);

            return ResultViewModel<List<PatientItemViewModel>>.Success(result);
        }
    }
}
