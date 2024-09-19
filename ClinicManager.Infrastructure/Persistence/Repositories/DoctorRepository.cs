using Azure.Core;
using ClinicManager.Core.Entities;
using ClinicManager.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ClinicManager.Infrastructure.Persistence.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ClinicDbContext _context;

        public DoctorRepository(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync(string query = null)
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

            return await doctorsQuery.ToListAsync();
        }

        public async Task<Doctor> GetByIdAsync(int id)
        {
            return await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task UpdateASync(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();
        }
    }
}
