
using Domain;
using Domain.RequestEntity;
using HueCitApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IMailServices
    {
        Task SendEmailAsync(MailRequest mailRequest);


    }
}
