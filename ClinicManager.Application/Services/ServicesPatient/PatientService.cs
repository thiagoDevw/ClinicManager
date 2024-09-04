using ClinicManager.Api.Models.PatientsModels;
using ClinicManager.Application.Models;
using ClinicManager.Application.Models.PatientsModels;
using ClinicManager.Core.Entities;
using ClinicManager.Infrastructure.Persistence;

namespace ClinicManager.Application.Services.ServicesPatient
{
    public class PatientService : IPatientService
    {
        private readonly ClinicDbContext _context;
        public PatientService(ClinicDbContext context)
        {
            _context = context;
        }

        public ResultViewModel DeleteById(int id)
        {
            var patient = _context.Patients.SingleOrDefault(d => d.Id == id);

            if (patient == null)
            {
                return ResultViewModel<PatientViewModel>.Error("Paciente não encontrado.");
            }

            _context.Patients.Remove(patient);
            _context.SaveChanges();

            return ResultViewModel.Success();
        }

        public ResultViewModel<List<PatientItemViewModel>> GetAll(string query)
        {
            var patients = _context.Patients.AsQueryable();

             if (!string.IsNullOrWhiteSpace(query))
            {
                query = query.ToLower();
                patients = patients.Where(p =>
                    p.Name.ToLower().Contains(query) ||
                    p.LastName.ToLower().Contains(query) ||
                    p.Email.ToLower().Contains(query) ||
                    p.CPF.ToLower().Contains(query));
            }

            var result = patients
                .Select(p => PatientItemViewModel.FromEntity(p))
                .ToList();

            return ResultViewModel<List<PatientItemViewModel>>.Success(result);
        }

        public ResultViewModel<PatientViewModel> GetById(int id)
        {
            var patient = _context.Patients
                .FirstOrDefault(p => p.Id == id);

            if (patient == null)
            {
                return ResultViewModel<PatientViewModel>.Error("Paciente não encontrado.");
            }

            var patientViewModel = PatientViewModel.FromEntity(patient);

            return ResultViewModel<PatientViewModel>.Success(patientViewModel);
        }

        public ResultViewModel<int> Insert(CreatePatientsInputModel model)
        {
            if (model == null)
            {
                return ResultViewModel<int>.Error("Os dados do paciente são obrigatórios.");
            }

            if (_context.Patients.Any(p => p.CPF == model.CPF))
            {
                return ResultViewModel<int>.Error("Paciente com este CPF já existe.");
            }

            var patientInsert = new Patient
            {
                Name = model.Name,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                Phone = model.Phone,
                Email = model.Email,
                CPF = model.CPF,
                BloodType = model.BloodType,
                Height = model.Height,
                Weight = model.Weight,
                Address = model.Address
            };

            _context.Patients.Add(patientInsert);
            _context.SaveChanges();

            return ResultViewModel<int>.Success(patientInsert.Id);
        }

        public ResultViewModel Update(int id, UpdatePatientsInputModel model)
        {
            if (model == null)
            {
                return ResultViewModel<int>.Error("Dados do paciente não fornecidos.");
            }

            var existingPatient = _context.Patients.Find(id);
            if (existingPatient == null)
            {
                return ResultViewModel<int>.Error($"Paciente com ID {id} não encontrado.");
            }

            existingPatient.Name = model.Name;
            existingPatient.LastName = model.LastName;
            existingPatient.DateOfBirth = model.DateOfBirth;
            existingPatient.Phone = model.Phone;
            existingPatient.Email = model.Email;
            existingPatient.CPF = model.CPF;
            existingPatient.BloodType = model.BloodType;
            existingPatient.Height = model.Height;
            existingPatient.Weight = model.Weight;
            existingPatient.Address = model.Address;

            _context.Patients.Update(existingPatient);
            _context.SaveChanges();

            return ResultViewModel.Success();
        }
    }
}
