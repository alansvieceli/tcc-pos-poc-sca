using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Shared.Extensions
{
    public static class LogServiceCollectionExtension
    {
        public static IServiceCollection ConfigureLogging(this IServiceCollection factory)
        {
            factory.AddLogging(builder => {
                builder.AddConsole().AddDebug();
            });

            return factory;
        }
    }
}
