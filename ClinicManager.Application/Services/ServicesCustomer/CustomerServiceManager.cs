using ClinicManager.Api.Models.CustomerModels;
using ClinicManager.Application.Models;
using ClinicManager.Application.Models.CustomerModels;
using ClinicManager.Core.Entities;
using ClinicManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ClinicManager.Application.Services.ServicesCustomer
{
    public class CustomerServiceManager : ICustomerService
    {
        private readonly ClinicDbContext _context;
        public CustomerServiceManager(ClinicDbContext context)
        {
            _context = context;
        }

        public ResultViewModel DeleteById(int id)
        {
            var customerService = _context.CustomerServices.Find(id);

            if (customerService == null)
            {
                return ResultViewModel.Error("Atendimento não encontrado.");
            }

            _context.CustomerServices.Remove(customerService);
            _context.SaveChanges();

            return ResultViewModel.Success();
        }

        public ResultViewModel<List<CustomerItemViewModel>> GetAll(string search = ""/*, int page = 1, int pageSize = 3*/)
        {
            var query = _context.CustomerServices
                    .Include(cs => cs.Patient)
                    .Include(cs => cs.Doctor)
                    .Include(cs => cs.Service)
                    .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(c => c.Patient.Name.ToLower().Contains(search) ||
                                         c.Doctor.Name.ToLower().Contains(search) ||
                                         c.Service.Name.ToLower().Contains(search));
            }

            // Logging para verificar os resultados filtrados
            var filteredCount = query.Count();
            Console.WriteLine($"Número de registros após o filtro: {filteredCount}");

            var totalRecords = query.Count();

            var customers = query
                .OrderBy(c => c.Patient.Name)
                /*.Skip((page - 1) * pageSize)
                .Take(pageSize)*/
                .Select(c => new CustomerItemViewModel(
                    c.Id,
                    c.Patient.Name,
                    c.Doctor.Name,
                    c.Service.Name,
                    c.Agreement
                ))
                .ToList();

            var result = new
            {
                TotalRecords = totalRecords,
                /*Page = page,
                PageSize = pageSize,*/
                Data = customers
            };

            return ResultViewModel<List<CustomerItemViewModel>>.Success(customers);
        }

        public ResultViewModel<CustomerViewModel> GetById(int id)
        {
            var customer = _context.CustomerServices
                .Where(c => c.Id == id)
                .Select(c => new CustomerViewModel
                {
                    Id = c.Id,
                    PatientName = c.Patient.Name,
                    DoctorName = c.Doctor.Name,
                    ServiceName = c.Service.Name,
                    Agreement = c.Agreement,
                    Start = c.Start,
                    End = c.End,
                    TypeService = c.TypeService
                })
                .FirstOrDefault();

            if (customer == null)
            {
                return ResultViewModel<CustomerViewModel>.Error("Atendimento não encontrado.");
            }

            return ResultViewModel<CustomerViewModel>.Success(customer);
        }

        public ResultViewModel<int> Insert(CreateCustomerInputModel model)
        {
            var customerService = new CustomerService
            {
                PatientId = model.PatientId,
                DoctorId = model.DoctorId,
                ServiceId = model.ServiceId,
                Agreement = model.Agreement,
                Start = model.Start,
                End = model.End,
                TypeService = model.TypeService
            };

            _context.CustomerServices.Add(customerService);
            _context.SaveChanges();

            return ResultViewModel<int>.Success(customerService.Id);
        }

        public ResultViewModel Update(UpdateCustomerInputModel model)
        {
            var customerService = _context.CustomerServices.Find(model.Id);

            if (customerService == null)
            {
                return ResultViewModel.Error("Atendimento não encontrado.");
            }

            customerService.PatientId = model.PatientId;
            customerService.DoctorId = model.DoctorId;
            customerService.ServiceId = model.ServiceId;
            customerService.Agreement = model.Agreement;
            customerService.Start = model.Start;
            customerService.End = model.End;
            customerService.TypeService = model.TypeService;

            _context.CustomerServices.Update(customerService);
            _context.SaveChanges();

            return ResultViewModel.Success();
        }
    }
}
