using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.NuGet.Push;
using Cake.Common.Tools.NuGet;
using Cake.Common.Tools.NuGet.Push;
using Cake.Core;
using Cake.Frosting;

namespace DefiantCode.Cake.Frosting.Tasks
{
    [Dependency(typeof(DotNetCorePack))]
    public sealed class NugetPush : FrostingTask<DotNetCoreContext>
    {
        public override void Run(DotNetCoreContext context)
        {
            var nugetSettings = new DotNetCoreNuGetPushSettings
            {
                ApiKey = context.NugetDefaultPushSourceApiKey,
                Source = context.NugetDefaultPushSourceUrl
            };

            if (string.IsNullOrEmpty(nugetSettings.ApiKey))
                throw new CakeException("NugetDefaultPushSourceApiKey was not set!");

            context.DotNetCoreNuGetPush(".\\artifacts\\*.nupkg", nugetSettings);
        }


    }

}