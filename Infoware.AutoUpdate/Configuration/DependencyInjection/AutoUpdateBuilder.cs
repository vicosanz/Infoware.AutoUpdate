using System;
using Microsoft.Extensions.DependencyInjection;

namespace Infoware.AutoUpdate.Configuration
{
    public class AutoUpdateBuilder : IAutoUpdateBuilder
    {
        public IServiceCollection Services { get; }

        public AutoUpdateBuilder(IServiceCollection services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }
    }
}
