using Azure.Core;
using ClinicManager.Core.Entities;
using ClinicManager.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClinicManager.Infrastructure.Persistence.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ClinicDbContext _context;

        public ServiceRepository(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Service service)
        {
            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var service = await _context.Services.FindAsync(id);

            if (service == null)
            {
                _context.Services.Remove(service);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Service>> GetAllAsync(string query = null)
        {
            var services = _context.Services.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query))
            {
                var lowerQuery = query.ToLower();
                services = services.Where(s =>
                    s.Name.ToLower().Contains(lowerQuery) ||
                    s.Description.ToLower().Contains(lowerQuery));
            }

            return await services.ToListAsync();
        }

        public async Task<Service> GetByIdAsync(int id)
        {
            return await _context.Services.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task UpdateAsync(Service service)
        {
            _context.Services.Update(service);
            await _context.SaveChangesAsync();
        }
    }
}
