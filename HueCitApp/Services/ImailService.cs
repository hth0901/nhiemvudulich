using HueCitApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HueCitApp.Services
{
    public interface ImailService
    {
    
        Task SendWelcomeEmailAsync(MailRequest _request);
    }
}
