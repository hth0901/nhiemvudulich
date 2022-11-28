using API.Services;
using Application.Core;
using Application.DiemGiaoDich;

using Dapper;
using Domain.HueCit;
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
         
            List<string> lstResult = new List<string>();
            SYS_SettingMail info = new SYS_SettingMail();
            string spName = "SP_MailGetsMailTo";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
            {
            
                connection.Open();
                var result = await connection.QueryAsync<string>(new CommandDefinition(spName, null, commandType: System.Data.CommandType.StoredProcedure));
                //var check = result;
                if (result != null && result.Any() && result.FirstOrDefault() != null)
                {
                    lstResult = result.FirstOrDefault().Split(',').ToList();
                }

                var res = await connection.QueryAsync<SYS_Setting>(new CommandDefinition("SP_MailGetsSetting", commandType: System.Data.CommandType.StoredProcedure));
                foreach (var r in res)
                {
                    if (r.ID == 4)
                    {
                        info.SendEmail = r.GiaTri;
                    }
                    else if (r.ID == 5)
                    {
                        info.SendPassword = r.GiaTri;
                    }
                    else if (r.ID == 6)
                    {
                        info.SendDisplayName = r.GiaTri;
                    }
                    else if (r.ID == 7)
                    {
                        info.ToEmail = r.GiaTri;
                    }
                    else if (r.ID == 10)
                    {
                        info.SendHost = r.GiaTri;
                    }
                    else if (r.ID == 11)
                    {
                        info.SendTitle = r.GiaTri;
                    }
                    else if (r.ID == 12)
                    {
                        info.SendContent = r.GiaTri;
                    }
                    else if (r.ID == 13)
                    {
                        info.SendPort = r.GiaTri;
                    }
                }
            }

            int soluong = lstResult.Count();

            if (soluong > 0 && info != null)
            {
                //timer
                for (int i = 0; i < soluong; i++)
                {
                    using (var connection = new SqlConnection(_configuration.GetConnectionString("HuecitConnection")))
                    {

                        connection.Open();
                        DynamicParameters dynamicParameters = new DynamicParameters();
                        dynamicParameters.Add("@Mail", lstResult[i]);
                        dynamicParameters.Add("@Time", DateTime.Now);
                        var result = await connection.QueryAsync<MailLog>(new CommandDefinition("SP_MailLogs", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure));
                    }
                    request.ToEmail = lstResult[i];
                    await _mailService.SendWelcomeEmailAsync(request, info);
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
