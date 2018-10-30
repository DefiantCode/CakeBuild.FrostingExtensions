using Cake.Common.Tools.NuGet;
using Cake.Common.Tools.NuGet.Install;
using Cake.Core;

namespace DefiantCode.Cake.Frosting.Utilities
{
    public static class NugetToolInstaller
    {
        public static void InstallNugetTool(this ICakeContext context, string package, string version)
        {
            context.NuGetInstall(package, new NuGetInstallSettings
            {
                Version = version,
                ExcludeVersion = true,
                OutputDirectory = "./tools"
            });
        }
    }


}