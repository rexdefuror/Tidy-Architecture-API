using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NetLore.Application.Read.Helpers;
using NetLore.Application.Write.Helpers;
using NetLore.Infrastructure.Pipelines;

namespace NetLore.Infrastructure.Extensions
{
    public static class MediatorRegistrationExtensions
    {
        public static IServiceCollection AddMediatorWithRequests(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddMediatR(options =>
            {
                options.AsTransient();
            },
                ReadAssemblyHelper.Get(),
                WriteAssemblyHelper.Get());

            services.Decorate(typeof(IRequestHandler<,>), typeof(LoggingPipeline<,>));
            return services;
        }
    }
}
