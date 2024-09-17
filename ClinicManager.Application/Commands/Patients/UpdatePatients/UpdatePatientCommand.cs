using ClinicManager.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.CommandsPatients.UpdatePatients
{
    public class UpdatePatientCommand : IRequest<ResultViewModel>
    {
        public UpdatePatientCommand(int id, string name, string lastName, DateTime dateOfBirth, string phone, string email, string cPF, string bloodType, double height, double weight, string address)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Phone = phone;
            Email = email;
            CPF = cPF;
            BloodType = bloodType;
            Height = height;
            Weight = weight;
            Address = address;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string BloodType { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string Address { get; set; }
    }
}
