using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infoware.AutoUpdate.GitHub.Options;
using Infoware.AutoUpdate.GitHub.Services;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AutoUpdateGitHubBuilderExtensionsCore
    {
        public static IAutoUpdateBuilder AddRequiredPlatformServicesGitHub(this IAutoUpdateBuilder builder)
        {
            builder.Services.AddOptions();
            builder.Services.AddSingleton(
                resolver => resolver.GetRequiredService<IOptions<AutoUpdateGitHubOptions>>().Value);

            return builder;
        }

        public static IAutoUpdateBuilder AddDefaultServicesGitHub(this IAutoUpdateBuilder builder)
        {
            builder.Services.AddTransient<IGitHubService, GitHubService>();
            return builder;

        }

    }
}
