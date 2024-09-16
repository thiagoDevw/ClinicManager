using ClinicManager.Application.Models;
using ClinicManager.Application.Models.DoctorModels;
using MediatR;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ClinicManager.Application.Queries.QueriesDoctors
{
    public class GetAllDoctorsQuery : IRequest<ResultViewModel<List<DoctorItemViewModel>>>
    {
        public GetAllDoctorsQuery(string query)
        {
            Query = query;
        }

        public string Query { get; set; }
    }
}
