using ClinicManager.Core.Entities;

namespace ClinicManager.Core.Repositories
{
    public interface IPatientRepository
    {
        Task<Patient> GetByIdAsync(int id);
        Task<List<Patient>> GetAllAsync(string query = null);
        Task<bool> PatientExistsAsync(string cpf);
        Task AddAsync(Patient patient);
        Task UpdateAsync(Patient patient);
        Task DeleteAsync(Patient patient);
    }
}
