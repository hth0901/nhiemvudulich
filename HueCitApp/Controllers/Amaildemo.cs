using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using MailKit;
using HueCitApp.Models;
using HueCitApp.Services;
using Microsoft.AspNetCore.Hosting;

namespace HueCitApp.Controllers
{
  
    public class Amaildemo : BaseApiController
    {
        private readonly ImailService _mailService;
        private readonly IWebHostEnvironment _webHostEnvironment;
  
        public Amaildemo(IWebHostEnvironment hostingEnvironment, ImailService mailService) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
            _mailService = mailService;
        }

   
        [AllowAnonymous]
        [HttpPost("welcome")]
        public async Task<IActionResult> SendWelcomeEmail([FromBody] MailRequest _request)
        {
            try
            {
                await _mailService.SendWelcomeEmailAsync(_request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
