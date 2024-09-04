using ClinicManager.Api.Models.ServiceModels;
using ClinicManager.Application.Models;
using ClinicManager.Application.Models.ServiceModels;

namespace ClinicManager.Application.Services.ServicesService
{
    public interface IService
    {
        ResultViewModel<List<ServiceItemViewModel>> GetAll(string search = "");
        ResultViewModel<ServiceViewModel> GetById(int id);
        ResultViewModel<int> Insert(CreateServiceInputModel model);
        ResultViewModel Update(int id, UpdateServiceInputModel model);
        ResultViewModel DeleteById(int id);
    }
}
