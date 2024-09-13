using ClinicManager.Application.Models;
using ClinicManager.Infrastructure.Persistence;
using MediatR;

namespace ClinicManager.Application.Commands.CommandsDoctors.DeleteDoctor
{
    public class DeleteDoctorHandler : IRequestHandler<DeleteDoctorCommand, ResultViewModel<int>>
    {
        private readonly ClinicDbContext _context;

        public DeleteDoctorHandler(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<int>> Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
        {
            var doctor = await _context.Doctors.FindAsync(request.DoctorId);
            
            if (doctor == null)
            {
                return ResultViewModel<int>.Error("Médico não encontrado.");
            }

            _context.Doctors.Remove(doctor);

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
                return ResultViewModel<int>.Success(doctor.Id);
            }
            catch (Exception ex)
            {
                return ResultViewModel<int>.Error($"Erro ao deletar o médico: {ex.Message}");
            }
        }
    }
}
