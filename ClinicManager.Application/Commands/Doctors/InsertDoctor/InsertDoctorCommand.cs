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
        public InsertDoctorCommand(string name, string lastName, DateTime dateOfBirth, string phone, string email, string cPF, string bloodType, string address, string specialty, string cRM)
        {
            Name = name;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Phone = phone;
            Email = email;
            CPF = cPF;
            BloodType = bloodType;
            Address = address;
            Specialty = specialty;
            CRM = cRM;
        }

        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string BloodType { get; set; }
        public string Address { get; set; }
        public string Specialty { get; set; }
        public string CRM { get; set; }
    }
}
