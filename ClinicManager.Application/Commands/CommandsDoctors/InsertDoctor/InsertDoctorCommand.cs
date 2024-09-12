using ClinicManager.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.CommandsDoctors.InsertDoctor
{
    public class InsertDoctorCommand : IRequest<ResultViewModel<int>>
    {

    }
}
