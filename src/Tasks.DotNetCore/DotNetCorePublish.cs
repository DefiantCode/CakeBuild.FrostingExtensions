using Cake.Common.Diagnostics;
using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Pack;
using Cake.Common.Tools.DotNetCore.Publish;
using Cake.Core;
using Cake.Frosting;
using System.Linq;

namespace DefiantCode.Cake.Frosting.Tasks
{
    [Dependency(typeof(DotNetCoreBuild))]
    public sealed class DotNetCorePublish : FrostingTask<DotNetCoreContext>
    {
        public override void Run(DotNetCoreContext context)
        {
            foreach (var project in context.Projects.Where(x => x.ProjectParserResult.IsNetCore && x.ProjectParserResult.NetCore.TargetFrameworks.Any(f => f.StartsWith("netcoreapp"))))
            {
                context.Information("Packaging project {0} with version: {1}", project.ProjectParserResult.NetCore.PackageId, context.BuildVersion.Version.FullSemVer);

                context.DotNetCorePublish(project.ProjectPath.FullPath, new DotNetCorePublishSettings
                {
                    Configuration = context.Configuration,
                    OutputDirectory = context.Artifacts.Combine(project.ProjectParserResult.NetCore.PackageId)
                });
            }
        }
    }
}