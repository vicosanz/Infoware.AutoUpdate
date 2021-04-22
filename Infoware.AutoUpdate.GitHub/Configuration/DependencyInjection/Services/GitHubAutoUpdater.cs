using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Infoware.AutoUpdate.GitHub.Options;
using Infoware.AutoUpdate.Options;
using Infoware.AutoUpdate.Services;

namespace Infoware.AutoUpdate.GitHub.Services
{
    public class GitHubAutoUpdater : AutoUpdater
    {
        private AutoUpdateGitHubOptions _options;
        private IGitHubService _gitHubService;
        private Installer _updateFound;
        private Task<string> _taskDownload;
        private string _fileDownloaded;

        public GitHubAutoUpdater(AutoUpdateGitHubOptions options, IGitHubService gitHubService)
        {
            _options = options;
            _gitHubService = gitHubService;
        }

        public override async Task<Installer> CheckUpdatesAsync(Assembly assembly)
        {
            var installers = await _gitHubService.GetInstallersAsync();
            if (installers is null)
            {
                throw new Exception("Error loading last release assets from GitHub repository");
            }
            var parsed = installers
                .Where(x => Regex.Match(x.Name, _options.RegExpInstaller).Success)
                .Select(x => ParseInstaller(x.Name))
                .ToList();

            if (parsed is null)
            {
                return null;
            }

            _updateFound =
                parsed.Where(x =>
                    x.Name.Equals(assembly.GetName().Name, StringComparison.InvariantCultureIgnoreCase) &&
                    x.Version.CompareTo(assembly.GetName().Version) > 0 &&
                    Helpers.CompatibilityArchitecture(x.Architecture, assembly)
                )
                .OrderByDescending(x => Helpers.ArchitectureWeight(x.Architecture))
                .OrderByDescending(x => x.Version)
                .FirstOrDefault();

            return _updateFound;
        }

        public override async Task DownloadUpdateAsync()
        {
            if (_updateFound is null)
            {
                throw new Exception("First check updates using CheckUpdatesAsync");
            }

            _taskDownload = _gitHubService.Download(_updateFound);
            _fileDownloaded = await _taskDownload;
        }

        public override void PerformUpdate()
        {
            if (!_taskDownload.IsCompleted)
            {
                _taskDownload.Wait();
                _fileDownloaded = _taskDownload.Result;
            }
            Process.Start(
                new ProcessStartInfo()
                {
                    UseShellExecute = true,
                    FileName = _fileDownloaded
                }
            ) ;
        }

        private Installer ParseInstaller(string name)
        {
            var match = Regex.Match(name, _options.RegExpInstaller);
            if (match.Success)
            {
                return new Installer()
                {
                    FileName = name,
                    Name = match.Groups[1].Value,
                    Version = Version.Parse(match.Groups[2].Value),
                    Configuration = match.Groups[3].Value,
                    Architecture = match.Groups[4].Value
                };
            }
            return null;
        }
    }
}
