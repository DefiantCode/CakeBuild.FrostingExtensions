using Cake.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Tool;
using Cake.Core.IO;
using Cake.Common.Diagnostics;

namespace DefiantCode.Cake.Frosting.Utilities
{
    public static class DotNetToolInstaller
    {
        public static void InstallDotNetTool(this DotNetCoreContext context, string package, string version = null)
        {
            var args = new ProcessArgumentBuilder();
            args.Append("tool");
            args.Append("install");
            args.Append("-g");
            args.Append(package);
            if(version != null)
                args.AppendSwitch("--version", version);
            var p = context.ProcessRunner.Start("dotnet", new ProcessSettings { Arguments = args, RedirectStandardError = true });
            p.WaitForExit();
            context.Information(string.Join("\n", p.GetStandardError()));
        }
    }
}
