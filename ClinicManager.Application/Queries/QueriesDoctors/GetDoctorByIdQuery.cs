﻿using ClinicManager.Api.Models.DoctorModels;
using ClinicManager.Application.Models;
using MediatR;

namespace ClinicManager.Application.Queries.QueriesDoctors
{
    public class GetDoctorByIdQuery : IRequest<ResultViewModel<DoctorViewModel>>
    {
        public GetDoctorByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
