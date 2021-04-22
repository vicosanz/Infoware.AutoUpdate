using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infoware.AutoUpdate.Services
{
    public abstract class AutoUpdater : IAutoUpdater
    {

        public abstract Task<Installer> CheckUpdatesAsync(Assembly assembly);
        public abstract Task DownloadUpdateAsync();
        public abstract void PerformUpdate();
    }
}
