using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SchoolManager.API.Filters;
using SchoolManager.Application;
using SchoolManager.Application.Classes.Queries.GetAllClasses;
using SchoolManager.Application.Common.Interfaces;
using SchoolManager.Infrastructure;
using SchoolManager.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SchoolManager.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddApplication();
            services.AddInfrastructure(Configuration);


            services.AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>())
                .AddFluentValidation()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SchoolManager.API", Version = "v1",Description = "Web API with Clean Architecture and MediatR" });
                c.EnableAnnotations();
                /*var xfile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xpath = Path.Combine(AppContext.BaseDirectory, xfile);
                c.IncludeXmlComments(xpath);*/
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ISchoolDbSeeder seeder)
        {

            seeder.Seed();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SchoolManager.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
