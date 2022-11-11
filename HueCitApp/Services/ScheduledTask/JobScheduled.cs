﻿using API.Services;
using Application.Core;
using Application.DiemGiaoDich;

using Dapper;
using Domain.RequestEntity;
using HueCitApp.Models;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HueCitApp.Services.ScheduledTask
{
    [DisallowConcurrentExecution]
    public class JobScheduled : IJob
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ILogger<JobScheduled> _logger;
        private readonly IOptions<MailSettings> _mailSettings;
        private readonly ImailService _mailService;
        private readonly IConfiguration _configuration;

        public JobScheduled(ILogger<JobScheduled> logger, IWebHostEnvironment hostingEnvironment, ImailService mailService,IConfiguration config)
        {
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
            _mailService = mailService;
            _configuration = config;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            //
             
          
            MailRequest request= new MailRequest();
         
            var lstResult = new List<string>(); 
            string spName = "SP_EMAIL_QuanTracMoiTruong";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
            {
            
                connection.Open();
                var result = await connection.QueryAsync<string>(new CommandDefinition(spName, null, commandType: System.Data.CommandType.StoredProcedure));
                //var check = result;
                lstResult = result.ToList();
              }

            int soluong = lstResult.Count();
            
            if (soluong > 0)
            {

                //timer
                for (int i = 0; i < soluong; i++)
                {

                    request.ToEmail = lstResult[i];
                    await _mailService.SendWelcomeEmailAsync(request);



                }
            }

     
            //await _mailService.SendWelcomeEmailAsync(request);

            //MailService tammail = new MailService(_mailSettings);
            //   var EmailScheduledw = new EmailScheduled();
            //   var tam = await EmailScheduledw.Handle();
            //    for(int i=0;i<tam.Value.Count;i++)
            //{   MailRequest check= new MailRequest();
            //    check.Subject = "aloalo";
            //    check.ToName=tam.Value[i];
            //    check.ToEmail = tam.Value[i];
            //    check.Body = "<h1>Email sended</h1>";
            //   await tammail.SendEmailAsync(check);
            //    _logger.LogInformation("Hello world!");


            //}



        }
    }
}