
using HueCitApp.Services;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace HueCitApp.Models
{
    public class MailRequest 
    {
        public string ToEmail { get; set; } = "tranngocrin12@gmail.com";
        public string UserName { get; set; } = "rin";

    }
}
