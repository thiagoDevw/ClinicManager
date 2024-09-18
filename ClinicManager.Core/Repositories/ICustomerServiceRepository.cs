using ClinicManager.Core.Entities;

namespace ClinicManager.Core.Repositories
{
    public interface ICustomerServiceRepository
    {
        Task<CustomerService> GetByIdAsync(int id);
        Task<(int TotalRecords, List<CustomerService> CustomerServices)> GetAllAsync(string search = null, int? page = null, int? pageSize = null);
        Task<int> AddAsync(CustomerService customerService);
        Task UpdateAsync(CustomerService customerService);
        Task<bool> DeleteAsync(int id);
    }
}
