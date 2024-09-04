using ClinicManager.Api.Models.DoctorModels;
using ClinicManager.Application.Models;
using ClinicManager.Application.Models.DoctorModels;
using ClinicManager.Core.Entities;
using ClinicManager.Infrastructure.Persistence;

namespace ClinicManager.Application.Services.ServicesDoctor
{
    public class DoctorService : IDoctorService
    {
        private readonly ClinicDbContext _context;
        public DoctorService(ClinicDbContext context)
        {
            _context = context;
        }

        public ResultViewModel DeleteById(int id)
        {
            var doctor = _context.Doctors.SingleOrDefault(d => d.Id == id);

            if (doctor == null)
            {
                return ResultViewModel.Error("Médico não encontrado.");
            }

            _context.Doctors.Remove(doctor);
            _context.SaveChanges();

            return ResultViewModel.Success();
        }

        public ResultViewModel<List<DoctorItemViewModel>> GetAll(string query)
        {
            var doctorsQuery = _context.Doctors.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query))
            {
                query = query.ToLower();
                doctorsQuery = doctorsQuery.Where(d =>
                    d.Name.ToLower().Contains(query) ||
                    d.LastName.ToLower().Contains(query) ||
                    d.Email.ToLower().Contains(query) ||
                    d.CPF.ToLower().Contains(query) ||
                    d.CRM.ToLower().Contains(query));
            }

            var doctors = doctorsQuery
                .Select(d => DoctorItemViewModel.FromEntity(d))
                .ToList();

            return ResultViewModel<List<DoctorItemViewModel>>.Success(doctors);
        }

        public ResultViewModel<DoctorViewModel> GetById(int id)
        {
            var doctor = _context.Doctors
                .FirstOrDefault(d => d.Id == id);

            if (doctor == null)
            {
                return ResultViewModel<DoctorViewModel>.Error("Médico não encontrado.");
            }

            var doctorViewModel = DoctorViewModel.FromEntity(doctor);

            return ResultViewModel<DoctorViewModel>.Success(doctorViewModel);
        }

        public ResultViewModel<int> Insert(CreateDoctorInputModel model)
        {
            if (model == null)
            {
                return ResultViewModel<int>.Error("Os dados do médico são obrigatórios.");
            }

            if (string.IsNullOrWhiteSpace(model.Name) ||
                string.IsNullOrWhiteSpace(model.LastName) ||
                string.IsNullOrWhiteSpace(model.Email) ||
                string.IsNullOrWhiteSpace(model.CPF) ||
                string.IsNullOrWhiteSpace(model.CRM))
            {
                return ResultViewModel<int>.Error("Alguns campos obrigatórios estão ausentes.");
            }

            var doctor = new Doctor
            {
                Name = model.Name,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                Phone = model.Phone,
                Email = model.Email,
                CPF = model.CPF,
                BloodType = model.BloodType,
                Address = model.Address,
                Specialty = model.Specialty,
                CRM = model.CRM
            };

            _context.Doctors.Add(doctor);
            _context.SaveChanges();


            return ResultViewModel<int>.Success(doctor.Id);

        }

        public ResultViewModel Update(int id, UpdateDoctorInputModel model)
        {
            if (model == null)
            {
                return ResultViewModel<int>.Error("Os dados do médico são obrigatórios.");
            }

            var doctor = _context.Doctors.SingleOrDefault(d => d.Id == id);

            if (doctor == null)
            {
                return ResultViewModel<int>.Error("Médico não encontrado.");

            }

            doctor.Name = model.Name;
            doctor.LastName = model.LastName;
            doctor.DateOfBirth = model.DateOfBirth;
            doctor.Phone = model.Phone;
            doctor.Email = model.Email;
            doctor.CPF = model.CPF;
            doctor.BloodType = model.BloodType;
            doctor.Address = model.Address;
            doctor.Specialty = model.Specialty;
            doctor.CRM = model.CRM;

            _context.SaveChanges();


            return ResultViewModel.Success();
        }
    }
}
