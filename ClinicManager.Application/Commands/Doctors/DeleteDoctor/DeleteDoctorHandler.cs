using ClinicManager.Application.Models;
using ClinicManager.Core.Repositories;
using MediatR;

namespace ClinicManager.Application.Commands.CommandsDoctors.DeleteDoctor
{
    public class DeleteDoctorHandler : IRequestHandler<DeleteDoctorCommand, ResultViewModel<int>>
    {
        private readonly IDoctorRepository _repository;

        public DeleteDoctorHandler(IDoctorRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<int>> Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
        {
            var doctor = await _repository.GetByIdAsync(request.DoctorId);
            
            if (doctor == null)
            {
                return ResultViewModel<int>.Error("Médico não encontrado.");
            }

            try
            {
                await _repository.DeleteAsync(request.DoctorId);
                return ResultViewModel<int>.Success(request.DoctorId);
            }
            catch (Exception ex)
            {
                return ResultViewModel<int>.Error($"Erro ao deletar o médico: {ex.Message}");
            }
        }
    }
}
