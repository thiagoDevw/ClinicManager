using Azure.Core;
using ClinicManager.Application.Models;
using ClinicManager.Infrastructure.Persistence;
using MediatR;

namespace ClinicManager.Application.Commands.CommandsPatients.UpdatePatients
{
    public class UpdatePatientHandler : IRequestHandler<UpdatePatientCommand, ResultViewModel>
    {
        private readonly ClinicDbContext _context;

        public UpdatePatientHandler(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return ResultViewModel<int>.Error("Dados do paciente não fornecidos.");
            }

            var existingPatient = await _context.Patients.FindAsync(request.Id);

            if (existingPatient == null)
            {
                return ResultViewModel<int>.Error($"Paciente com ID {request.Id} não encontrado.");
            }

            existingPatient.Name = request.Name;
            existingPatient.LastName = request.LastName;
            existingPatient.DateOfBirth = request.DateOfBirth;
            existingPatient.Phone = request.Phone;
            existingPatient.Email = request.Email;
            existingPatient.CPF = request.CPF;
            existingPatient.BloodType = request.BloodType;
            existingPatient.Height = request.Height;
            existingPatient.Weight = request.Weight;
            existingPatient.Address = request.Address;

            _context.Patients.Update(existingPatient);
            await _context.SaveChangesAsync(cancellationToken);

            return ResultViewModel.Success();
        }
    }
}
