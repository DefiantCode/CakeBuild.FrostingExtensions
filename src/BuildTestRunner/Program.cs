using Cake.Frosting;
using DefiantCode.Cake.Frosting;
using DefiantCode.Cake.Frosting.Tasks;
using System;

namespace BuildTestRunner
{
    public class Program : IFrostingStartup
    {
        public static int Main(string[] args)
        {
            // Create the host.
            var host = new CakeHostBuilder()
                .WithArguments(args)
                .UseStartup<Program>()
                .Build();

            // Run the host.
            return host.Run();
        }

        public void Configure(ICakeServices services)
        {
            services.UseAssembly(typeof(DotNetCoreBuild).Assembly);
            services.UseContext<DotNetCoreContext>();
            services.UseLifetime<DotNetCoreLifetime>();
            //services.UseContext<DynamicContext>();
            //services.UseLifetime<DynamicLifetime>();
            services.UseWorkingDirectory("../../");
        }
    }
}
