using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infoware.AutoUpdate.Services;
using Octokit;

namespace Infoware.AutoUpdate.GitHub.Services
{
    public interface IGitHubService
    {
        Task<IReadOnlyList<ReleaseAsset>> GetInstallersAsync();
        Task<Release> GetLastReleaseAsync();
        Task<string> Download(Installer updateFound);
    }
}
