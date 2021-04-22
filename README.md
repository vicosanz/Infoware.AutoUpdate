# Infoware.AutoUpdate
 
Implement update service for .net applications.

Support dependecy injection.

### Get it!
[![NuGet Badge](https://buildstats.info/nuget/Infoware.AutoUpdate)](https://www.nuget.org/packages/Infoware.AutoUpdate/)

Extension for GitHub based updater
[![NuGet Badge](https://buildstats.info/nuget/Infoware.AutoUpdate.GitHub)](https://www.nuget.org/packages/Infoware.AutoUpdate.GitHub/)
Use GitHub assets as repository Installers.

Abstraction allow extension. 

You can create your own extensions.
Example:
- Update using Shared folder.
- Update based on FTP Service.
- Etc.

## Buyme a coffee
:coffee: https://www.paypal.com/paypalme/vicosanzdev?locale.x=es_XC

### How to use it

```csharp
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddLogging(configure => configure.AddConsole());
                    services.AddDbContext<BaseContext>(options =>
                    {
                        options.UseSqlServer(
                          ....
                        );
                    });
                    services.AddAutoUpdateGitHub((options)=>
                    {
                        options.Assembly = Assembly.GetExecutingAssembly();
                        options.GitHubUser = "vicosanz";
                        options.GitHubRepository = "FancyRepository";
                    });
                });
```

GitHub Extension:
- Assembly: Allow get app name, version, etc.
- GitHubUser: GitHub User
- GitHubRepository: GitHub Repository

This repository must have almost one push and almost one Release created. Check https://docs.github.com/es/github/administering-a-repository/managing-releases-in-a-repository
GitHub extension find the last release based on Creation Date (aka Latest Release) and look for updates available into Assets uploaded (not source code zips!)
You must upload your MSI assets into the Latest Release in the following format:

MyFancyApp-1.0.0.0-Release-x86.msi

Where:
- MyFancyApp must match with Assembly.GetExecutingAssembly().GetName().Name (commonly the startup project into your solution)
- 1.0.0.0 is the version of your startup project when you create installer
- Release is the configuration of the compiler
- x86 is the architecture supported for your installer

You can inject Updater service into constructor
```csharp
        IAutoUpdater _autoUpdater;
        ...

        public XController(IAutoUpdater autoUpdater)
        {
            _autoUpdater = autoUpdater;
```
In order to check updates you must:
```csharp
        private Installer _updateAvailable;
        ....

        private async void CheckUpdates()
        {
            if (_updateAvailable is null)
            {
                _updateAvailable = await _updater.CheckUpdatesAsync(Assembly.GetEntryAssembly());
                if (_updateAvailable != null)
                {
                    ShowGreatMessage("An update was found. Downloading...");
                    await _updater.DownloadUpdateAsync();
                    ShowGreatMessage("Update ready. Restart app to continue.");
                }
            }
        }
```
Depending on your installer could required to close app to implement the update
```csharp
        private void ClosedApp()
        {
            if (_updateAvailable != null)
            {
                _updater.PerformUpdate();
            }
        }
```
The Updater execute installer throught Process.Start and finishing.

