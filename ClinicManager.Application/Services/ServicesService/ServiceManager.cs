using ClinicManager.Api.Models.ServiceModels;
using ClinicManager.Application.Models;
using ClinicManager.Application.Models.ServiceModels;
using ClinicManager.Core.Entities;
using ClinicManager.Infrastructure.Persistence;
using System.ComponentModel.DataAnnotations;

namespace ClinicManager.Application.Services.ServicesService
{
    public class ServiceManager : IService
    {
        private readonly ClinicDbContext _context;
        public ServiceManager(ClinicDbContext context)
        {
            _context = context;
        }

        public ResultViewModel DeleteById(int id)
        {
            var service = _context.Services.SingleOrDefault(s => s.Id == id);

            if (service == null)
            {
                return ResultViewModel.Error("Serviço não encontrado.");
            }

            _context.Services.Remove(service);
            _context.SaveChanges();

            return ResultViewModel.Success();
        }

        public ResultViewModel<List<ServiceItemViewModel>> GetAll(string query)
        {
            query = query?.ToLower();

            var services = _context.Services
                .Where(s => string.IsNullOrEmpty(query) ||
                   s.Name.ToLower().Contains(query) ||
                   s.Description.ToLower().Contains(query))
                .Select(s => ServiceItemViewModel.FromEntity(s))
                .ToList();

            return ResultViewModel<List<ServiceItemViewModel>>.Success(services);
        }

        public ResultViewModel<ServiceViewModel> GetById(int id)
        {
            var service = _context.Services
                .FirstOrDefault(s => s.Id == id);

            if (service == null)
            {
                return ResultViewModel<ServiceViewModel>.Error("Paciente não encontrado.");
            }

            var serviceViewModel = ServiceViewModel.FromEntity(service);

           return ResultViewModel<ServiceViewModel>.Success(serviceViewModel);
        }

        public ResultViewModel<int> Insert(CreateServiceInputModel model)
        {
            if (model == null)
            {
                return ResultViewModel<int>.Error("Dados do serviço não fornecidos.");
            }

            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(model, null, null);
            if (!Validator.TryValidateObject(model, context, validationResults, true))
            {
                var errorMessage = string.Join("; ", validationResults.Select(vr => vr.ErrorMessage));
                return ResultViewModel<int>.Error(errorMessage);
            }

            if (_context.Services.Any(s => s.Name == model.Name))
            {
                return ResultViewModel<int>.Error("Já existe um serviço com este nome.");
            }

            var service = new Service
            {
                Name = model.Name,
                Description = model.Description,
                Value = model.Value,
                Duration = model.Duration
            };

            _context.Services.Add(service);
            _context.SaveChanges();

            return ResultViewModel<int>.Success(service.Id);
        }

        public ResultViewModel Update(int id, UpdateServiceInputModel model)
        {
            if (model == null)
            {
                return ResultViewModel.Error("Dados do serviço não fornecidos.");
            }

            if (id != model.Id)
            {
                return ResultViewModel.Error("Id do serviço não corresponde ao ID fornecido no modelo.");
            }

            var existingService = _context.Services.Find(id);
            if (existingService == null)
            {
                return ResultViewModel.Error($"Serviço com ID {id} não encontrado.");
            }

            existingService.Name = model.Name;
            existingService.Description = model.Description;
            existingService.Value = model.Value;
            existingService.Duration = model.Duration;

            _context.Services.Update(existingService);
            _context.SaveChanges();


            return ResultViewModel.Success();
        }
    }
}
