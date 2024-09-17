using ClinicManager.Application.Models;
using ClinicManager.Application.Models.PatientsModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ClinicManager.Application.Queries.Patients.GetAllPatients
{
    public class GetAllPatientsQuery : IRequest<ResultViewModel<List<PatientItemViewModel>>>
    {
        public GetAllPatientsQuery(string query)
        {
            Query = query;
        }

        public string Query { get; set; }
    }
}
