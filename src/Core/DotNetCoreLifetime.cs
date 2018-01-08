using Cake.Common;
using Cake.Common.Diagnostics;
using Cake.Frosting;
using System.Linq;
using Cake.Incubator;
using Cake.Common.Solution;
using Cake.Common.Build;
using Cake.Core.IO;
using DefiantCode.Cake.Frosting.Utilities;
using Cake.Core;

namespace DefiantCode.Cake.Frosting
{
    public class DotNetCoreLifetime : FrostingLifetime<DotNetCoreContext>
    {
        public override void Setup(DotNetCoreContext context)
        {
            GitVersionTool.Install(context);

            context.Target = context.Argument("target", "Default");
            context.Configuration = context.Argument("configuration", "Release");
            context.SolutionRoot = context.FileSystem.GetDirectory(".").Path.MakeAbsolute(context.Environment);
            context.BuildVersion = BuildVersion.Calculate(context);
            context.Artifacts = context.Argument("artifacts", "./artifacts");
            context.NugetDefaultPushSourceUrl = GetEnvOrArg(context, "nugetDefaultPushSourceUrl", "NUGET_DEFAULT_PUSH_SOURCE_URL");
            context.NugetDefaultPushSourceApiKey = GetEnvOrArg(context, "NUGET_DEFAULT_PUSH_SOURCE_URL_API_KEY", "nugetDefaultPushSourceApiKey");

            if (!context.HasArgument("solutionFilePath"))
            {
                var slnPaths = context.Globber.Match("*.sln", x => true).Where(x => !x.FullPath.EndsWith("Build.sln", System.StringComparison.OrdinalIgnoreCase));

                if (slnPaths != null && slnPaths.Count() == 1)
                    context.SolutionFilePath = slnPaths.Single().FullPath;
                else if (slnPaths != null && slnPaths.Count() > 1)
                    throw new CakeException("More than 1 sln files found. Specify the sln file to used with the solutionFilePath argument.");
            }
            else
                context.SolutionFilePath = context.Argument<string>("solutionFilePath", null);

            if (context.SolutionFilePath != null)
            {
                context.Information("Using solution: {0}", context.SolutionFilePath.FullPath);
                var slnResult = context.ParseSolution(context.SolutionFilePath);
                context.Projects = slnResult.Projects.Where(x => !x.Path.FullPath.EndsWith("Solution Items")).Select(x => new Project(x.Path, context.ParseProject(x.Path, context.Configuration))).ToList().AsReadOnly();
            }
            else
                context.Projects = new Project[0];

            var buildSystem = context.BuildSystem();
            context.IsLocalBuild = buildSystem.IsLocalBuild;

            context.DirectoriesToClean = new DirectoryPath[] { context.Artifacts };

            context.Verbose("\n\nDumping context...\n\n{0}", context.ToString());
        }
        private static string GetEnvOrArg(DotNetCoreContext context, string environmentVariable, string argumentName)
        {
            var arg = context.EnvironmentVariable(environmentVariable);
            if (string.IsNullOrWhiteSpace(arg))
            {
                arg = context.Argument<string>(argumentName, null);
            }
            return arg;
        }
    } 
}