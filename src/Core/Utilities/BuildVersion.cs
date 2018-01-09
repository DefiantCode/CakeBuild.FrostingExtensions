using Cake.Common.Diagnostics;
using Cake.Common.Tools.GitVersion;
using Cake.Core;
using Cake.Incubator;

namespace DefiantCode.Cake.Frosting.Utilities
{
    public class BuildVersion
    {
        public GitVersion Version { get; set; }

        public BuildVersion(GitVersion version)
        {
            Version = version;
        }

        public BuildVersion() : this(new GitVersion())
        {
        }

        public BuildVersion(string fullSemVer, string nugetVersionV2) : this(new GitVersion { FullSemVer = fullSemVer, NuGetVersionV2 = nugetVersionV2 })
        {

        }

        public static BuildVersion Calculate(IContext context)
        {
            if (context.Environment.Platform.IsUnix())
            {
                //TODO: Call GitVersion.exe with mono
                return new BuildVersion();
            }
            else
            {
                context.Information("Calculating semantic version...");

                if (!context.IsLocalBuild)
                {
                    // Run to set the version properties inside the CI server
                    context.GitVersion(new GitVersionSettings { OutputType = GitVersionOutput.BuildServer });
                }

                // Run in interactive mode to get the properties for the rest of the script
                var assertedversions = context.GitVersion(new GitVersionSettings { OutputType = GitVersionOutput.Json });

                if (string.IsNullOrWhiteSpace(assertedversions.MajorMinorPatch))
                {
                    throw new CakeException("Could not calculate version of build.");
                }

                context.Verbose("\n\nDumping GitVersion info...\n\n{0}\n\n", assertedversions.Dump());

                return new BuildVersion(assertedversions); 
            }
        }

        public override string ToString()
        {
            return Version.FullSemVer;
        }


    } 
}
