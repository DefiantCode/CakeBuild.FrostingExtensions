using Cake.Common.Diagnostics;
using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Pack;
using Cake.Core;
using Cake.Frosting;
using System.Linq;

namespace DefiantCode.Cake.Frosting.Tasks
{
    [Dependency(typeof(DotNetCoreClean))]
    [Dependency(typeof(DotNetCoreBuild))]
    public sealed class DotNetCorePack : FrostingTask<DotNetCoreContext>
    {
        public override void Run(DotNetCoreContext context)
        {
            foreach (var project in context.Projects.Where(x => x.ProjectParserResult.IsNetCore && x.ProjectParserResult.NetCore.IsPackable))
            {
                context.Information("Packaging project {0} with version: {1}",project.ProjectParserResult.NetCore.PackageId, context.BuildVersion.Version.FullSemVer);

                context.DotNetCorePack(project.ProjectPath.FullPath, new DotNetCorePackSettings()
                {
                    Configuration = context.Configuration,
                    NoBuild = true,
                    OutputDirectory = context.Artifacts,
                    IncludeSource = true,
                    IncludeSymbols = true,
                    ArgumentCustomization = args => args.Append("/p:Version={0}", context.BuildVersion.Version.NuGetVersionV2),
                    Verbosity = context.GetVerbosity()
                });
            }

        }
    } 
}