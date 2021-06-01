using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    static class Program
    {
        public static IServiceProvider Services;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Services = CreateHostBuilder(null).Build().Services;
            Application.Run(new Form1());
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddAutoUpdateGitHub((options) =>
                    {
                        options.Assembly = Assembly.GetExecutingAssembly();
                        options.GitHubUser = "vicosanz";
                        options.GitHubRepository = "Infoware.SRI.DocumentosElectronicosOfflineInstaller";
                    });
                });

    }
}
