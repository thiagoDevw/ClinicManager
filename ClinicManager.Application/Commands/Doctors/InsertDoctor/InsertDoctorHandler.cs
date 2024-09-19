using ClinicManager.Application.Models;
using ClinicManager.Core.Entities;
using ClinicManager.Core.Repositories;
using ClinicManager.Infrastructure.Persistence;
using MediatR;

namespace ClinicManager.Application.Commands.CommandsDoctors.InsertDoctor
{
    public class InsertDoctorHandler : IRequestHandler<InsertDoctorCommand, ResultViewModel<int>>
    {
        private readonly IDoctorRepository _repository;

        public InsertDoctorHandler(IDoctorRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<int>> Handle(InsertDoctorCommand request, CancellationToken cancellationToken)
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

            await _repository.AddAsync(doctor);

            return ResultViewModel<int>.Success(doctor.Id);
        }
    }
}
