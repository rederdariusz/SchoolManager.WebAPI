using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolManager.Application.Common.Interfaces;
using SchoolManager.Infrastructure.Persistance;
using System;

namespace SchoolManager.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SchoolDbContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("SchoolDbConnection"),
                    b => b.MigrationsAssembly(typeof(SchoolDbContext).Assembly.FullName));
            });

            services.AddScoped<ISchoolDbContext>(provider => provider.GetService<SchoolDbContext>());


            return services;
        }
    }
}
