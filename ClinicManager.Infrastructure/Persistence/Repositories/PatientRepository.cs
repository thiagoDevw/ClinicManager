using ClinicManager.Core.Entities;
using ClinicManager.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClinicManager.Infrastructure.Persistence.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ClinicDbContext _context;

        public PatientRepository(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<bool> PatientExistsAsync(string cpf)
        {
            return await _context.Patients.AnyAsync(p => p.CPF == cpf);
        }

        public async Task AddAsync(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Patient patient)
        {
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Patient>> GetAllAsync(string query = null)
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

            return await patients.ToListAsync();
        }

        public async Task<Patient> GetByIdAsync(int id)
        {
            return await _context.Patients.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateAsync(Patient patient)
        {
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
        }
    }
}
