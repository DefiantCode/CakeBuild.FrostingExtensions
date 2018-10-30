using Cake.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Tool;
using Cake.Core.IO;

namespace DefiantCode.Cake.Frosting.Utilities
{
    public static class DotNetToolInstaller
    {
        public static void InstallDotNetTool(this DotNetCoreContext context, string package, string version = null)
        {
            var args = new ProcessArgumentBuilder();
            args.Append("-g");
            args.Append(package);
            if (!string.IsNullOrEmpty(version))
                args.AppendSwitch("--version", version);

            //for some reason this alias wants a path to a csproj but it's never used, only the directory the file is in
            //so we create a filepath to the working directory and a non-existant csproj file
            var projectPath = context.Environment.WorkingDirectory + "dummy.csproj";
            context.DotNetCoreTool(projectPath, "tool install", args);  
        }

        //public static void InstallDotNetTool(string workingDirectory, string package, string version = null)
        //{
        //    var args = new ProcessArgumentBuilder();
        //    args.Append("-g");
        //    args.Append(package);
        //    if (!string.IsNullOrEmpty(version))
        //        args.AppendSwitch("--version", version);

        //    var toolRunner = new DotNetCoreToolRunner()
        //}
    }
}
