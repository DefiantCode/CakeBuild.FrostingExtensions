using Cake.Frosting;
using DefiantCode.Cake.Frosting;
using DefiantCode.Cake.Frosting.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build
{

    [TaskName("Default")]
    [Dependency(typeof(DotNetCorePack))]
    public class DefaultTask : FrostingTask<DotNetCoreContext>
    {
        public override void Run(DotNetCoreContext context)
        {

        }
    }

    [TaskName("push")]
    [Dependency(typeof(DotNetCoreNugetPush))]
    public class PushTask : FrostingTask<DotNetCoreContext>
    {
        public override void Run(DotNetCoreContext context)
        {

        }
    }
}
