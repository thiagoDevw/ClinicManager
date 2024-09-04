using ClinicManager.Api.Models.DoctorModels;
using ClinicManager.Application.Models;
using ClinicManager.Application.Models.DoctorModels;

namespace ClinicManager.Application.Services.ServicesDoctor
{
    public interface IDoctorService
    {
        ResultViewModel<List<DoctorItemViewModel>> GetAll(string search = "");
        ResultViewModel<DoctorViewModel> GetById(int id);
        ResultViewModel<int> Insert(CreateDoctorInputModel model);
        ResultViewModel Update(int id, UpdateDoctorInputModel model);
        ResultViewModel DeleteById(int id);
    }
}
