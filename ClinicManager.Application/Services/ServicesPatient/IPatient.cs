using ClinicManager.Api.Models.PatientsModels;
using ClinicManager.Application.Models;
using ClinicManager.Application.Models.PatientsModels;

namespace ClinicManager.Application.Services.ServicesPatient
{
    public interface IPatientService
    {
        ResultViewModel<List<PatientItemViewModel>> GetAll(string search = "");
        ResultViewModel<PatientViewModel> GetById(int id);
        ResultViewModel<int> Insert(CreatePatientsInputModel model);
        ResultViewModel Update(int id, UpdatePatientsInputModel model);
        ResultViewModel DeleteById(int id);
    }
}
