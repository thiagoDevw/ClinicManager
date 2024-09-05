using ClinicManager.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Services.ServicesEmail
{
    public interface IEmailSender
    {
        ResultViewModel SendEmail(string toEmail, string subject, string body);
    }
}
