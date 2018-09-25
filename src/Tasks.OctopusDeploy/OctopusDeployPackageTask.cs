using Cake.Frosting;
using DefiantCode.Cake.Frosting;
using Octopus.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tasks.OctopusDeploy
{
    [TaskName("octo-pack")]
    public class OctopusDeployPackageTask : FrostingTask<DotNetCoreContext>
    {
        public override void Run(DotNetCoreContext context)
        {
            
            var serverUrl = context.GetProperty<string>("octopusServerUrl");
            var apiKey = context.GetProperty<string>("octopusApiKey");
            var ep = new OctopusServerEndpoint(serverUrl, apiKey);
            using (var client = OctopusAsyncClient.Create(ep).GetAwaiter().GetResult())
            {
                var repo = client.CreateRepository();
                
            }
        }
    }
}
