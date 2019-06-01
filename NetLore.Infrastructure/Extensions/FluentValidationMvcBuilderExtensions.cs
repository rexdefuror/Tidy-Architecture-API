using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using NetLore.Application.Read.Helpers;
using NetLore.Application.Write.Helpers;
using System.Collections.Generic;
using System.Reflection;

namespace NetLore.Infrastructure.Extensions
{
    public static class FluentValidationMvcBuilderExtensions
    {
        public static IMvcBuilder AddFluentValidators(this IMvcBuilder builder)
        {
            builder.AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblies(new List<Assembly>
                {
                    ReadAssemblyHelper.Get(),
                    WriteAssemblyHelper.Get()
                });
            });
            return builder;
        }
    }
}
