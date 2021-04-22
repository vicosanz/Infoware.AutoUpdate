using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Infoware.AutoUpdate.Options;

namespace Infoware.AutoUpdate.GitHub.Options
{
    public class AutoUpdateGitHubOptions : IAutoUpdateOptions
    {
        public string URI { get; set; } = "https://github.com/";
        public string GitHubUser { get; set; }
        public string GitHubRepository { get; set; }
        public Assembly Assembly { get; set; }
        public bool IncludePreRelease { get; set; } = false;
        public string RegExpInstaller { get; set; } = @"(.+)-(\d*.\d*.\d*.\d*)-(\w+)-(\w+).msi";
    }
}
