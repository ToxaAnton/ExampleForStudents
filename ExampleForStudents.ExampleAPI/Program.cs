using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ExampleForStudents.ExampleAPI
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((context, loggerConfiguration) //configuration from appsettings
                    => loggerConfiguration.ReadFrom.Configuration(context.Configuration))
                // .UseSerilog((context, loggerConfiguration) 
                //     => loggerConfiguration.ReadFrom.Configuration(context.Configuration)
                //         .Enrich.WithEnvironment(Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT"))
                //         .Enrich.WithEnvironmentUserName()
                //         .Enrich.WithEnvironmentName()
                //         .Enrich.WithCorrelationId()
                //         .Enrich.WithAssemblyName()
                //         .Enrich.WithMemoryUsage()
                //         .Enrich.FromLogContext()
                //         .Enrich.WithProcessId()
                //         .Enrich.WithThreadId()
                //         .WriteTo.File(new JsonFormatter(), "logs.json",
                //             rollingInterval: RollingInterval.Day)
                //         .WriteTo.Seq("http://localhost:5341/"))
                // alternative configuration from code 
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}