using ClinicManager.Application.Models;
using MediatR;

namespace ClinicManager.Application.Commands.CommandsDoctors.DeleteDoctor
{
    public class DeleteDoctorCommand : IRequest<ResultViewModel<int>>
    {
        public int DoctorId { get; }

        public DeleteDoctorCommand(int doctorId)
        {
            DoctorId = doctorId;
        }
    }
}
