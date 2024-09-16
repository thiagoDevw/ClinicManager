using ClinicManager.Application.Models;
using ClinicManager.Core.Entities;
using ClinicManager.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ClinicManager.Application.Commands.CommandsServices.InsertService
{
    public class InsertServiceHandler : IRequestHandler<InsertServiceCommand, ResultViewModel<int>>
    {
        private readonly ClinicDbContext _context;

        public InsertServiceHandler(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<int>> Handle(InsertServiceCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return ResultViewModel<int>.Error("Dados do serviço não fornecidos.");
            }

            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(request, null, null);
            if (!Validator.TryValidateObject(request, context, validationResults, true))
            {
                var errorMessage = string.Join("; ", validationResults.Select(vr => vr.ErrorMessage));
                return ResultViewModel<int>.Error(errorMessage);
            }

            if (await _context.Services.AnyAsync(s => s.Name == request.Name, cancellationToken))
            {
                return ResultViewModel<int>.Error("Já existe um serviço com este nome.");
            }

            var service = new Service
            {
                Name = request.Name,
                Description = request.Description,
                Value = request.Value,
                Duration = request.Duration
            };

            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            return ResultViewModel<int>.Success(service.Id);
        }
    }
}
