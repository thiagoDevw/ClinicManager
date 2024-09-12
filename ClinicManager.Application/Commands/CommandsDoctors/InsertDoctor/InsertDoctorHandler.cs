using ClinicManager.Core.Entities;
using ClinicManager.Infrastructure.Persistence;
using MediatR;

namespace ClinicManager.Application.Commands.CommandsDoctors.InsertDoctor
{
    public class InsertDoctorHandler : IRequestHandler<InsertDoctorCommand, int>
    {
        private readonly ClinicDbContext _context;

        public InsertDoctorHandler(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(InsertDoctorCommand request, CancellationToken cancellationToken)
        {
            var doctor = new Doctor
            {
                Name = request.Name,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                Phone = request.Phone,
                Email = request.Email,
                CPF = request.CPF,
                BloodType = request.BloodType,
                Address = request.Address,
                Specialty = request.Specialty,
                CRM = request.CRM
            };

            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync(cancellationToken);

            return doctor.Id;
        }
    }
}
