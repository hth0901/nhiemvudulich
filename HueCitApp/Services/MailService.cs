using AutoMapper.Internal;
using HueCitApp.Models;

using Microsoft.Extensions.Options;
using System.IO;
using MailKit;
using MimeKit;
using System.Threading.Tasks;
using DinkToPdf;
using DinkToPdf.Contracts;
using MailKit.Net.Smtp;
using Application.DiaDiemAnUong;
using Domain.ResponseEntity;
using MediatR;
using System.Drawing.Printing;

using System.Threading;
using Application.Core;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Linq;
using API.Services;
using System;
using MimeKit.Text;
using Domain.HueCit;

namespace HueCitApp.Services
{
    public class MailService : ImailService
    {
        private readonly IOptions<MailSettings> _mailSettings;

        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings;
        }

        public async Task SendWelcomeEmailAsync(MailRequest _request, SYS_SettingMail settings)
        {
    

            var email = new MimeMessage();
            //email.Sender = MailboxAddress.Parse(_mailSettings.Value.Mail);
            email.Sender = MailboxAddress.Parse(settings.SendEmail);
            email.To.Add(MailboxAddress.Parse(_request.ToEmail));
            email.Subject = settings.SendTitle;
            var builder = new BodyBuilder();
            email.Body = new TextPart(TextFormat.Html) { Text= settings.SendContent };
           
            using var smtp = new SmtpClient();
            smtp.Connect(settings.SendHost, Int32.Parse(settings.SendPort), MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(settings.SendEmail, settings.SendPassword);

            await smtp.SendAsync(email);

            smtp.Disconnect(true);
        }
    }
}
