using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infoware.AutoUpdate.Services
{
    public interface IAutoUpdater
    {
        Task<Installer> CheckUpdatesAsync(Assembly assembly);
        void PerformUpdate();
        Task DownloadUpdateAsync();
    }
}
