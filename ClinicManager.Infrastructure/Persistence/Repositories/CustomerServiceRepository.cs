using ClinicManager.Core.Entities;
using ClinicManager.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClinicManager.Infrastructure.Persistence.Repositories
{
    public class CustomerServiceRepository : ICustomerServiceRepository
    {
        private readonly ClinicDbContext _context;

        public CustomerServiceRepository(ClinicDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddAsync(CustomerService customerService)
        {
            _context.CustomerServices.Add(customerService);
            await _context.SaveChangesAsync();

            return customerService.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var customerService = await _context.CustomerServices.FindAsync(id);
            if (customerService == null)
            {
                return false; 
            }

            _context.CustomerServices.Remove(customerService);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<(int TotalRecords, List<CustomerService> CustomerServices)> GetAllAsync(string search = null, int? page = null, int? pageSize = null)
        {
            var query = _context.CustomerServices
            .Include(cs => cs.Patient)
            .Include(cs => cs.Doctor)
            .Include(cs => cs.Service)
            .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(c => c.Patient.Name.ToLower().Contains(search.ToLower()) ||
                                         c.Doctor.Name.ToLower().Contains(search.ToLower()) ||
                                         c.Service.Name.ToLower().Contains(search.ToLower()));
            }

            var totalRecords = await _context.CustomerServices.CountAsync();
            var customerServices = await query
                .OrderBy(c => c.Patient.Name)
                .Skip((page ?? 1 - 1) * (pageSize ?? 10))
                .Take(pageSize ?? 10)
                .ToListAsync();

            return (totalRecords, customerServices);
        }

        public async Task<CustomerService> GetByIdAsync(int id)
        {
            var customerService = await _context.CustomerServices
            .Include(cs => cs.Patient)
            .Include(cs => cs.Doctor)
            .Include(cs => cs.Service)
            .FirstOrDefaultAsync(cs => cs.Id == id);

            return customerService;
        }

        public async Task UpdateAsync(CustomerService customerService)
        {
            _context.CustomerServices.Update(customerService);
            await _context.SaveChangesAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _context.CustomerServices.CountAsync();
        }
    }
}
