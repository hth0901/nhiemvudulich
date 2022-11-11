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
using Application.EmailDelay;
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

namespace HueCitApp.Services
{
    public class MailService : ImailService
    {
        private readonly IOptions<MailSettings> _mailSettings;

        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings;
        }
        //private readonly IOptions<MailSettings> _mailSettings;


        //public MailService(IOptions<MailSettings> mailSettings)
        //{
        //    _mailSettings = mailSettings;


        //}

        // public async Task SendEmailAsync(MailRequest mailRequest) 
        //{

        //        var email = new MimeMessage();
        //        email.Sender = MailboxAddress.Parse(_mailSettings.Value.Mail);
        //        email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
        //        email.Subject = mailRequest.Subject;
        //        var builder = new BodyBuilder();
        //        builder.HtmlBody = mailRequest.Body;
        //        email.Body = builder.ToMessageBody();
        //        using var smtp = new SmtpClient();
        //        smtp.Connect(_mailSettings.Value.Host, _mailSettings.Value.Port, MailKit.Security.SecureSocketOptions.StartTls);
        //        smtp.Authenticate(_mailSettings.Value.Mail, _mailSettings.Value.Password);
        //        await smtp.SendAsync(email);
        //        smtp.Disconnect(true);
        //    //throw new NotImplementedException();

        //}
        public async Task SendWelcomeEmailAsync(MailRequest _request)
        {
            string FilePath = "C:\\Users\\trann\\Downloads\\Document\\SendMailDemo\\SendMailDemo\\MailTemplate\\WelcomeTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[username]", _request.UserName).Replace("[email]", _request.ToEmail);

            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Value.Mail);
            email.To.Add(MailboxAddress.Parse(_request.ToEmail));
            email.Subject = $"Welcome {_request.UserName}";
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Value.Host, _mailSettings.Value.Port, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Value.Mail, _mailSettings.Value.Password);

            await smtp.SendAsync(email);

            smtp.Disconnect(true);
        }
    }
}
