using Cake.Common.IO;
using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.NuGet.Push;
using Cake.Core;
using Cake.Frosting;

namespace DefiantCode.Cake.Frosting.Tasks
{
    [Dependency(typeof(DotNetCorePack))]
    public sealed class DotNetCoreNugetPush : FrostingTask<DotNetCoreContext>
    {
        public override void Run(DotNetCoreContext context)
        {
            var nugetSettings = new DotNetCoreNuGetPushSettings
            {
                ApiKey = context.NugetDefaultPushSourceApiKey,
                Source = context.NugetDefaultPushSourceUrl
            };

            foreach (var package in context.GetFiles(context.Artifacts.FullPath + "/*.nupkg"))
            {
                context.DotNetCoreNuGetPush(package.FullPath, nugetSettings);
            }
        }

    }

}