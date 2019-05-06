using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace NetLore.Infrastructure.Extensions
{
    public static class AutoMapperRegistrationExtensions
    {
        public static IServiceCollection AddAutoMapperWithProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetAssembly(typeof(AutoMapperRegistrationExtensions)));
            return services;
        }
    }
}
