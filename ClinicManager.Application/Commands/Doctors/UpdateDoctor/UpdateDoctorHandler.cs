using ClinicManager.Application.Models;
using ClinicManager.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClinicManager.Application.Commands.CommandsDoctors.UpdateDoctor
{
    public class UpdateDoctorHandler : IRequestHandler<UpdateDoctorCommand, ResultViewModel<int>>
    {
        private readonly ClinicDbContext _context;

        public UpdateDoctorHandler(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<int>> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return ResultViewModel<int>.Error("Os dados do médico são obrigatórios.");
            }

            var doctor = await _context.Doctors.SingleOrDefaultAsync(d => d.Id == request.Id);

            if (doctor == null)
            {
                return ResultViewModel<int>.Error("Médico não encontrado.");

            }

            doctor.Name = request.Name;
            doctor.LastName = request.LastName;
            doctor.DateOfBirth = request.DateOfBirth;
            doctor.Phone = request.Phone;
            doctor.Email = request.Email;
            doctor.CPF = request.CPF;
            doctor.BloodType = request.BloodType;
            doctor.Address = request.Address;
            doctor.Specialty = request.Specialty;
            doctor.CRM = request.CRM;

            await _context.SaveChangesAsync(cancellationToken);


            return ResultViewModel<int>.Success(doctor.Id);
        }
    }
}
