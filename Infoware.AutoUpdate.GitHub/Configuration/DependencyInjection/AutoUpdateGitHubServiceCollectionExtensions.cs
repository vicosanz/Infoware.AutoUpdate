using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infoware.AutoUpdate.Configuration;
using Infoware.AutoUpdate.GitHub.Options;
using Infoware.AutoUpdate.GitHub.Services;
using Infoware.AutoUpdate.Options;
using Infoware.AutoUpdate.Services;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AutoUpdateGitHubServiceCollectionExtensions
    {
        public static IAutoUpdateBuilder AddAutoUpdateGitHub(this IServiceCollection services)
        {
            var builder = services.AddAutoUpdateBuilder();

            builder
                .AddRequiredPlatformServices()
                .AddRequiredPlatformServicesGitHub()
                .AddDefaultServices()
                .AddDefaultServicesGitHub();

            builder.Services.AddTransient<IAutoUpdater, GitHubAutoUpdater>();

            return builder;
        }

        public static IAutoUpdateBuilder AddAutoUpdateGitHub(this IServiceCollection services,
            Action<AutoUpdateGitHubOptions> setupAction)
        {
            services.Configure(setupAction);
            return services.AddAutoUpdateGitHub();
        }

        public static IAutoUpdateBuilder AddAutoUpdateGitHub(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AutoUpdateGitHubOptions>(configuration);
            return services.AddAutoUpdateGitHub();
        }

    }
}
