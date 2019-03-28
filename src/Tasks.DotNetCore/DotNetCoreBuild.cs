using Cake.Frosting;
using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Build;
using Cake.Core;
using Cake.Common.Diagnostics;

namespace DefiantCode.Cake.Frosting.Tasks
{
    public sealed class DotNetCoreBuild : FrostingTask<DotNetCoreContext>
    {
        public override void Run(DotNetCoreContext context)
        {
            context.Validate(ValidateOptions.Default, true);
            context.Information("Building projects with version: {0}", context.BuildVersion.Version.FullSemVer);
            context.DotNetCoreBuild(context.SolutionFilePath.FullPath, new DotNetCoreBuildSettings
            {
                Configuration = context.Configuration,
                ArgumentCustomization = args => args.Append("/p:Version={0}", context.BuildVersion.Version.FullSemVer),
                Verbosity = context.GetVerbosity()               
            });
        }

    } 
}