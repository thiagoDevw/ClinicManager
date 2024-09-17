using ClinicManager.Application.Models;
using ClinicManager.Infrastructure.Persistence;
using MediatR;

namespace ClinicManager.Application.Commands.CommandsPatients.DeletePatients
{
    public class DeletePatientHandler : IRequestHandler<DeletePatientCommand, ResultViewModel<int>>
    {
        private readonly ClinicDbContext _context;
        public DeletePatientHandler(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<int>> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
        {
            var patient = await _context.Patients.FindAsync(request.Id);

            if (patient == null)
            {
                return ResultViewModel<int>.Error("Paciente não encontrado.");
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return ResultViewModel<int>.Success(patient.Id);
        }
    }
}
