using ClinicManager.Application.Models;
using ClinicManager.Core.Entities;
using ClinicManager.Core.Repositories;
using MediatR;

namespace ClinicManager.Application.Commands.CommandsPatients.InsertPatients
{
    public class InsertPatientHandler : IRequestHandler<InsertPatientCommand, ResultViewModel<int>>
    {
        private readonly IPatientRepository _repository;

        public InsertPatientHandler(IPatientRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<int>> Handle(InsertPatientCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return ResultViewModel<int>.Error("Os dados do paciente são obrigatórios.");
            }

            if (await _repository.PatientExistsAsync(request.CPF))
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

            _repository.AddAsync(patientInsert);

            return ResultViewModel<int>.Success(patientInsert.Id);
        }
    }
}
