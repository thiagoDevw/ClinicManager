﻿using ClinicManager.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.CommandsDoctors.InsertDoctor
{
    public class InsertDoctorCommand : IRequest<int>
    {
        public InsertDoctorCommand(int id, string name, string lastName, DateTime dateOfBirth, string phone, string email, string cPF, string bloodType, string address, string specialty, string cRM)
        {
            Id = id;
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

        public int Id { get; set; }
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