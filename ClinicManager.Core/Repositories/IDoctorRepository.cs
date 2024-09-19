using ClinicManager.Core.Entities;

namespace ClinicManager.Core.Repositories
{
    public interface IDoctorRepository
    {
        Task<Doctor> GetByIdAsync(int id);
        Task<IEnumerable<Doctor>> GetAllAsync(string query = null);
        Task AddAsync(Doctor doctor);
        Task UpdateASync(Doctor doctor);
        Task DeleteAsync(int id);
    }
}
