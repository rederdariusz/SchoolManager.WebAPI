using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SchoolManager.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Reflection;

namespace SchoolManager.Application
{
    public static class DependencyInjection
    {

        
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton(GetConfiguredMappingConfig());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<IMapper, ServiceMapper>();

            


            return services;
        }

        private static TypeAdapterConfig GetConfiguredMappingConfig()
        {
            var config = TypeAdapterConfig.GlobalSettings;
            IList<IRegister> registers = config.Scan(Assembly.GetExecutingAssembly());
            config.Apply();
            return config;
        }
    }
}
