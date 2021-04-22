using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Infoware.AutoUpdate.GitHub.Options;
using Infoware.AutoUpdate.Services;
using Octokit;

namespace Infoware.AutoUpdate.GitHub.Services
{
    public class GitHubService : IGitHubService
    {
        private AutoUpdateGitHubOptions _options;
        private GitHubClient _github;
        private Release _lastRelease;

        public GitHubService(AutoUpdateGitHubOptions options)
        {
            _options = options;
            _github = new Octokit.GitHubClient(
                new Octokit.ProductHeaderValue(
                    "Infoware.AutoUpdate.GitHub",
                    Assembly.GetExecutingAssembly().GetName().Version.ToString()
                )
            );

        }

        public async Task<string> Download(Installer installer)
        {
            var url = 
                _lastRelease.Assets
                    .Where(x => string.Equals(x.Name, installer.FileName, StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefault().Url;
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new Exception($"Error loading download url for {installer.FileName}");
            }
            var targetFile = Path.Combine(Path.GetTempPath(), _options.Assembly.GetName().Name, installer.FileName);
            var response = await _github.Connection.Get<object>(new Uri(url), new Dictionary<string, string>(), "application/octet-stream");
            File.Delete(targetFile);
            await File.WriteAllBytesAsync(targetFile, (byte[])response.HttpResponse.Body);
            return targetFile;
        }

        public async Task<IReadOnlyList<ReleaseAsset>> GetInstallersAsync()
        {
            _lastRelease = await GetLastReleaseAsync();
            return _lastRelease?.Assets;
        }

        public async Task<Release> GetLastReleaseAsync()
        {
            var releases = await _github.Repository.Release.GetAll(_options.GitHubUser, _options.GitHubRepository);
            return releases
                .Where(x => _options.IncludePreRelease || !x.Prerelease)
                .OrderByDescending(x => x.PublishedAt)
                .First();
        }
    }
}
