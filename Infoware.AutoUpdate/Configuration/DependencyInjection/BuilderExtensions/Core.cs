using Microsoft.Extensions.Options;
using Infoware.AutoUpdate.Options;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AutoUpdateBuilderExtensionsCore
    {
        public static IAutoUpdateBuilder AddRequiredPlatformServices(this IAutoUpdateBuilder builder)
        {
            builder.Services.AddOptions();
            builder.Services.AddSingleton(
                resolver => resolver.GetRequiredService<IOptions<AutoUpdateOptions>>().Value);

            return builder;
        }

        public static IAutoUpdateBuilder AddDefaultServices(this IAutoUpdateBuilder builder)
        {
            return builder;

        }

    }
}
