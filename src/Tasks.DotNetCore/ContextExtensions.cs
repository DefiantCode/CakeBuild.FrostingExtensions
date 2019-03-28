using Cake.Common.Tools.DotNetCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DefiantCode.Cake.Frosting.Tasks
{
    public static class ContextExtensions
    {
        public static DotNetCoreVerbosity GetVerbosity(this DotNetCoreContext context)
        {
            switch (context.Log.Verbosity)
            {
                case global::Cake.Core.Diagnostics.Verbosity.Quiet:
                    return DotNetCoreVerbosity.Quiet;
                case global::Cake.Core.Diagnostics.Verbosity.Minimal:
                    return DotNetCoreVerbosity.Minimal;
                case global::Cake.Core.Diagnostics.Verbosity.Normal:
                    return DotNetCoreVerbosity.Minimal;
                case global::Cake.Core.Diagnostics.Verbosity.Verbose:
                    return DotNetCoreVerbosity.Detailed;
                case global::Cake.Core.Diagnostics.Verbosity.Diagnostic:
                    return DotNetCoreVerbosity.Diagnostic;
                default:
                    return DotNetCoreVerbosity.Minimal;
            }
        }
    }
}
