using Cake.Common.Diagnostics;
using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Pack;
using Cake.Common.Tools.DotNetCore.Publish;
using Cake.Core;
using Cake.Frosting;
using DefiantCode.Cake.Frosting.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace DefiantCode.Cake.Frosting.Tasks
{
    public sealed class DotNetCorePublish : FrostingTask<DotNetCoreContext>
    {
        public override void Run(DotNetCoreContext context)
        {
            var publishedProjects = new List<Project>();

            foreach (var project in context.Projects.Where(x => x.ProjectParserResult.IsNetCore && x.ProjectParserResult.NetCore.TargetFrameworks.Any(f => f.StartsWith("netcoreapp"))))
            {
                context.Information("Publishing  project {0} with version: {1}", project.ProjectParserResult.NetCore.PackageId, context.BuildVersion.Version.FullSemVer);
                project.PublishedOutputDirectory = context.Artifacts.Combine(project.ProjectParserResult.NetCore.PackageId);
                var settings = new DotNetCorePublishSettings
                {
                    Configuration = context.Configuration,
                    OutputDirectory = project.PublishedOutputDirectory
                };
                var runtimeIdentifiers = project.ProjectParserResult.NetCore.RuntimeIdentifiers;
                if (runtimeIdentifiers.Any())
                {
                    foreach (var runtime in runtimeIdentifiers)
                    {
                        settings.Runtime = runtime;
                        context.DotNetCorePublish(project.ProjectPath.FullPath, settings);
                    }
                }
                else
                    context.DotNetCorePublish(project.ProjectPath.FullPath, settings);

                publishedProjects.Add(project);
            }

            context.Outputs.PublishedProjects = publishedProjects;
        }
    }
}