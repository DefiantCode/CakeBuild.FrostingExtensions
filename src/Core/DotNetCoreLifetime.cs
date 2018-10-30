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
using System;
using Cake.Core.Diagnostics;

namespace DefiantCode.Cake.Frosting
{

    public class DotNetCoreLifetime : FrostingLifetime<DotNetCoreContext>
    {
        private static object _lock = new object();
        private static ILifetimeActions _lifetimeActions;

        public static void RegisterActions(ILifetimeActions lifetimeActions)
        {
            lock (_lock)
                _lifetimeActions = lifetimeActions;
        }

        public override void Setup(DotNetCoreContext context)
        {
            context.Information("v{0}", GetType().Assembly.GetName().Version);
            _lifetimeActions?.BeforeSetup(context);
            if(!context.DisableGitVersion)
                GitVersionTool.InstallGitVersion(context);

            context.Target = context.Argument("target", "Default");
            context.Configuration = context.Argument("configuration", "Release");
            context.SolutionRoot = context.FileSystem.GetDirectory(".").Path.MakeAbsolute(context.Environment);
            if(!context.DisableGitVersion)
                context.BuildVersion = BuildVersion.Calculate(context);

            context.Artifacts = context.Argument("artifacts", "./artifacts");
            context.NugetDefaultPushSourceUrl = GetEnvOrArg(context, "NUGET_DEFAULT_PUSH_SOURCE_URL", "nugetDefaultPushSourceUrl");
            context.NugetDefaultPushSourceApiKey = GetEnvOrArg(context, "NUGET_DEFAULT_PUSH_SOURCE_API_KEY", "nugetDefaultPushSourceApiKey");

            if (!context.HasArgument("solutionFilePath"))
            {
                var slnPaths = context.Globber.Match("*.sln", x => true).Where(x => !x.FullPath.EndsWith("Build.sln", System.StringComparison.OrdinalIgnoreCase));

                if (slnPaths != null && slnPaths.Count() == 1)
                    context.SolutionFilePath = slnPaths.Single().FullPath;
                else if (slnPaths != null && slnPaths.Count() > 1)
                    throw new CakeException("More than 1 sln files found. Specify the sln file to used with the solutionFilePath argument.");
            }
            else
                context.SolutionFilePath = context.SolutionFilePath != null && context.SolutionFilePath.IsSolution() ? context.SolutionFilePath : context.Argument<string>("solutionFilePath", null);

            if (context.SolutionFilePath != null)
            {
                context.Information("Using solution: {0}", context.SolutionFilePath.FullPath);
                context.Debug("Parsing solution...");
                var slnResult = context.ParseSolution(context.SolutionFilePath);
                context.Debug("Parsing projects...");
                context.Projects = slnResult.Projects.Where(x => {
                    context.Debug("Project: Name = {0}; Type = {1}; IsSolutionFolder = {2}", x.Name, x.Type, x.IsSolutionFolder());
                    return !x.IsSolutionFolder();
                    }).Select(x => new Project(x.Path, context.ParseProject(x.Path, context.Configuration))).ToList().AsReadOnly();
            }
            else
                context.Projects = new Project[0];

            var buildSystem = context.BuildSystem();
            context.IsLocalBuild = buildSystem.IsLocalBuild;
            if(buildSystem.IsRunningOnTeamCity)
            {
                //do specific TC stuff here
                
            }

            context.DirectoriesToClean = new DirectoryPath[] { context.Artifacts };

            _lifetimeActions?.AfterSetup(context);

            context.Verbose("\n\nDumping context...\n\n{0}", context.ToString());
        }

        public override void Teardown(DotNetCoreContext context, ITeardownContext info)
        {
            _lifetimeActions?.BeforeTeardown(context, info);
            _lifetimeActions?.AfterTeardown(context, info);
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