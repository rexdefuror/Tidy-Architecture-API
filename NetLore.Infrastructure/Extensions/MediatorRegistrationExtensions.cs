using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NetLore.Application.Read.Helpers;
using NetLore.Application.Write.Helpers;

namespace NetLore.Infrastructure.Extensions
{
    public static class MediatorRegistrationExtensions
    {
        public static IServiceCollection AddMediatorWithRequests(this IServiceCollection services)
        {
            services.AddMediatR(options =>
            {
                options.AsTransient();
            },
                ReadAssemblyHelper.Get(),
                WriteAssemblyHelper.Get());
            return services;
        }
    }
}
