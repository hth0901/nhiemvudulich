using Domain;
using HueCitApp.Services.ScheduledTask;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace HueCitApp
{
    public class Program
    {
        //public static void Main(string[] args)
        //{
        //    CreateHostBuilder(args).Build().Run();
        //}
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<DataContext>();
                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                await context.Database.MigrateAsync();
                //await Seed.SeedData(context, userManager);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "co gi do sai sai roi");
            }

            await host.RunAsync();
        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureServices((hostContext, services) =>
            {
                //// Add the required Quartz.NET services
               // services.AddQuartz(q =>
               // {
               //     // Use a Scoped container to create jobs. I'll touch on this later
               //     q.UseMicrosoftDependencyInjectionScopedJobFactory();

               //     // Create a "key" for the job
               //     var jobKey = new JobKey("HelloWorldJob");

               //     // Register the job with the DI container
               //     q.AddJob<JobScheduled>(opts => opts.WithIdentity(jobKey).Build());

               //     // Create a trigger for the job
               //     q.AddTrigger(opts => opts
               //         .ForJob(jobKey) // link to the HelloWorldJob
               //         .WithIdentity("HelloWorldJob-trigger") // give the trigger a unique name
               //         .WithDailyTimeIntervalSchedule((x =>
               //x.WithIntervalInMinutes(42).Build())));
               //     // run every 5 seconds
               // });

               // // Add the Quartz.NET hosted service

               // services.AddQuartzHostedService(
               //     q => q.WaitForJobsToComplete = true);

                // other config
            })
                .ConfigureLogging(logBuilder => {
                    logBuilder.ClearProviders(); // removes all providers from LoggerFactory
                    logBuilder.AddConsole();
                    //logBuilder.AddTraceSource("Information, ActivityTracing"); // Add Trace listener provider
                    logBuilder.AddTraceSource("Error"); // Add Trace listener provider
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
