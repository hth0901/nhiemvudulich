
using HueCitApp.Services;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace HueCitApp.Models
{
    public class MailRequest 
    {
        public string ToEmail { get; set; }
        public string UserName { get; set; } = "demo";

    }
}
