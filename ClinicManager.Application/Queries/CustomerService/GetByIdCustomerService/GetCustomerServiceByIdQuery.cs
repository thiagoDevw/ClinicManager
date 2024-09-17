using Azure.Core;
using ClinicManager.Api.Models.CustomerModels;
using ClinicManager.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Queries.CustomerService.GetByIdCustomerService
{
    public class GetCustomerServiceByIdQuery : IRequest<ResultViewModel<CustomerViewModel>>
    {
        public GetCustomerServiceByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
