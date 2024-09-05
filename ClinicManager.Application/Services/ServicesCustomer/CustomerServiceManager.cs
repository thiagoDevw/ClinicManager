using ClinicManager.Api.Models.CustomerModels;
using ClinicManager.Application.Models;
using ClinicManager.Application.Models.CustomerModels;
using ClinicManager.Application.Services.ServicesEmail;
using ClinicManager.Core.Entities;
using ClinicManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ClinicManager.Application.Services.ServicesCustomer
{
    public class CustomerServiceManager : ICustomerService
    {
        private readonly ClinicDbContext _context;
        private readonly IEmailSender _emailSender;
        public CustomerServiceManager(ClinicDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
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
            try
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

                // Buscar o paciente pelo PatientId
                var patient = _context.Patients.FirstOrDefault(p => p.Id == model.PatientId);

                if (patient == null)
                {
                    return ResultViewModel<int>.Error("Paciente não encontrado.");
                }

                // Obter o email do paciente
                string toEmail = patient.Email;

                // Definir assunto e mensagem de email
                string subject = "Novo atendimento criado";
                string message = $"Olá {patient.Name}, seu novo atendimento foi criado com sucesso para o dia {customerService.Start}.";

                // Enviar o email
                var emailResult = _emailSender.SendEmail(toEmail, subject, message);

                if (!emailResult.IsSucess)
                {
                    Console.WriteLine($"Erro ao enviar email: {emailResult.Message}");
                    return ResultViewModel<int>.Error($"Atendimento criado, mas houve um erro ao enviar o email: {emailResult.Message}");
                }

                _context.SaveChanges();

                return ResultViewModel<int>.Success(customerService.Id);
            }
            catch (Exception ex)
            {
                return ResultViewModel<int>.Error($"Erro ao criar o atendimento: {ex.Message}");
            }

            
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
