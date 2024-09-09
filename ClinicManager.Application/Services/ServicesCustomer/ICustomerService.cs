using ClinicManager.Api.Models.CustomerModels;
using ClinicManager.Application.Models;
using ClinicManager.Application.Models.CustomerModels;

namespace ClinicManager.Application.Services.ServicesCustomer
{
    public interface ICustomerService
    {
        ResultViewModel<List<CustomerItemViewModel>> GetAll(string search = ""/*, int page = 1, int pageSize = 3*/);
        ResultViewModel<CustomerViewModel> GetById(int id);
        Task<ResultViewModel<int>> Insert(CreateCustomerInputModel model);
        ResultViewModel Update(UpdateCustomerInputModel model);
        ResultViewModel DeleteById(int id);
    }
}
