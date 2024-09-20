using ClinicManager.Core.Entities;

namespace ClinicManager.Core.Repositories
{
    public interface IServiceRepository
    {
        Task<List<Service>> GetAllAsync(string query = null);
        Task<Service> GetByIdAsync(int id);
        Task AddAsync(Service service);
        Task UpdateAsync(Service service);
        Task DeleteAsync(int id);
    }
}
