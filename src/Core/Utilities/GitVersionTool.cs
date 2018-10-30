using Cake.Core;

namespace DefiantCode.Cake.Frosting.Utilities
{
    public static class GitVersionTool
    {
        public static void InstallGitVersion(this ICakeContext context, string package = "GitVersion.CommandLine", string version = "3.6.2")
        {
            context.InstallNugetTool(package, version);
        }
    }
}