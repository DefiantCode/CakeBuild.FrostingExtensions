using System;
using System.Collections.Generic;
using System.Text;
using Cake.Frosting;
using DefiantCode.Cake.Frosting;

namespace Tasks.OctopusDeploy
{
    [TaskName("octo-release")]
    public class OctopusDeployReleaseTask : FrostingTask<DotNetCoreContext>
    {
        public override void Run(DotNetCoreContext context)
        {
            
        }
    }
}
