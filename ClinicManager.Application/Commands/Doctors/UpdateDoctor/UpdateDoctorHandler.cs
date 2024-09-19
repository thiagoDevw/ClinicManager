using ClinicManager.Application.Models;
using ClinicManager.Core.Repositories;
using MediatR;

namespace ClinicManager.Application.Commands.CommandsDoctors.UpdateDoctor
{
    public class UpdateDoctorHandler : IRequestHandler<UpdateDoctorCommand, ResultViewModel<int>>
    {
        private readonly IDoctorRepository _repository;

        public UpdateDoctorHandler(IDoctorRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<int>> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return ResultViewModel<int>.Error("Os dados do médico são obrigatórios.");
            }

            var doctor = await _repository.GetByIdAsync(request.Id);

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

            await _repository.UpdateASync(doctor);


            return ResultViewModel<int>.Success(doctor.Id);
        }
    }
}
