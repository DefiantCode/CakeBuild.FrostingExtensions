using Cake.Common.Tools.GitVersion;
using Cake.Common.Tools.NuGet;
using Cake.Common.Tools.NuGet.Install;
using Cake.Core;

namespace DefiantCode.Cake.Frosting.Utilities
{
    public class GitVersionTool
    {
        public static void Install(ICakeContext context, string package = "GitVersion.CommandLine", string version = "3.6.2")
        {
            context.NuGetInstall(package, new NuGetInstallSettings
            {
                Version = version,
                ExcludeVersion = true,
                OutputDirectory = "./tools"
            });
        }

        public static GitVersionRunner Init(IContext context)
        {
            return new GitVersionRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools, context.Log);
        }
    }


}