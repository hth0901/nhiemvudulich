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

namespace HueCitApp.Services
{
    public class MailService : ImailService
    {
        private readonly IOptions<MailSettings> _mailSettings;

        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings;
        }

        public async Task SendWelcomeEmailAsync(MailRequest _request)
        {
    

            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Value.Mail);
            email.To.Add(MailboxAddress.Parse(_request.ToEmail));
            email.Subject = "Cảnh báo chất lượng môi trường không khí" ;
            var builder = new BodyBuilder();
            email.Body = new TextPart(TextFormat.Html) { Text="<h3>Chỉ số chất lượng môi trường không khí AQI đang vượt mức 1 - Hệ thống Hỗ trợ Điều hành Du lịch Thông minh Thừa Thiên Huế</h3>" };
           
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Value.Host, _mailSettings.Value.Port, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Value.Mail, _mailSettings.Value.Password);

            await smtp.SendAsync(email);

            smtp.Disconnect(true);
        }
    }
}
