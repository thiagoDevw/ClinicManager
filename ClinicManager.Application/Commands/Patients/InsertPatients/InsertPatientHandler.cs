using ClinicManager.Application.Models;
using ClinicManager.Core.Entities;
using ClinicManager.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClinicManager.Application.Commands.CommandsPatients.InsertPatients
{
    public class InsertPatientHandler : IRequestHandler<InsertPatientCommand, ResultViewModel<int>>
    {
        private readonly ClinicDbContext _context;

        public InsertPatientHandler (ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<int>> Handle(InsertPatientCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return ResultViewModel<int>.Error("Os dados do paciente são obrigatórios.");
            }

            if (await _context.Patients.AnyAsync(p => p.CPF == request.CPF, cancellationToken))
            {
                return ResultViewModel<int>.Error("Paciente com este CPF já existe.");
            }

            var patientInsert = new Patient
            {
                Name = request.Name,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                Phone = request.Phone,
                Email = request.Email,
                CPF = request.CPF,
                BloodType = request.BloodType,
                Height = request.Height,
                Weight = request.Weight,
                Address = request.Address
            };

            _context.Patients.Add(patientInsert);
            await _context.SaveChangesAsync(cancellationToken);

            return ResultViewModel<int>.Success(patientInsert.Id);
        }
    }
}
