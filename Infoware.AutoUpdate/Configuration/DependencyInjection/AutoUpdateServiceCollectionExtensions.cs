using System;
using Infoware.AutoUpdate.Options;
using Infoware.AutoUpdate.Configuration;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AutoUpdateServiceCollectionExtensions
    {
        public static IAutoUpdateBuilder AddAutoUpdateBuilder(this IServiceCollection services)
        {
            return new AutoUpdateBuilder(services);
        }

        public static IAutoUpdateBuilder AddAutoUpdate(this IServiceCollection services)
        {
            var builder = services.AddAutoUpdateBuilder();

            builder
                .AddRequiredPlatformServices()
                .AddDefaultServices();

            return builder;
        }

        public static IAutoUpdateBuilder AddAutoUpdate(this IServiceCollection services,
            Action<AutoUpdateOptions> setupAction)
        {
            services.Configure(setupAction);
            return services.AddAutoUpdate();
        }

        public static IAutoUpdateBuilder AddAutoUpdate(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AutoUpdateOptions>(configuration);
            return services.AddAutoUpdate();
        }

    }
}
