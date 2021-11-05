using System.Text.Json.Serialization;
using ExampleForStudents.Core.Abstractions;
using ExampleForStudents.Core.MapperProfiles;
using ExampleForStudents.Core.Services;
using ExampleForStudents.Domain.Interfaces;
using ExampleForStudents.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Serilog;

namespace ExampleForStudents.ExampleAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(options => options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ExampleAPI",
                    Description = "Api for students to show basic principles of Clean Architecture"
                }))
                .AddAutoMapper(typeof(CarMapperProfile).Assembly)
                .AddScoped<ICarsService, CarsService>()
                .AddSingleton<ICarsRepository, CarsRepository>()
                .AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger()
                .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Example API for students"))
                .UseSerilogRequestLogging()
                .UseExceptionHandler(error => error.Run(async context =>
                {
                    var feature =
                        context.Features
                            .Get<IExceptionHandlerPathFeature>(); //here you can get the actual exception 'feature.Error'

                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                        { Error = "Ups... Something went wrong" }));
                }))
                .UseHttpsRedirection()
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}