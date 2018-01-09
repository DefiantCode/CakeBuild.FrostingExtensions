using Cake.Common;
using Cake.Frosting;
using DefiantCode.Cake.Frosting;
using DefiantCode.Cake.Frosting.Tasks;
using DefiantCode.Cake.Frosting.Utilities;

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
        services.UseWorkingDirectory("..");

        RegisterLifetimeActions();
    }

    private void RegisterLifetimeActions()
    {
        DotNetCoreLifetime.RegisterBeforeSetupAction(ctx =>
        {
            if (ctx.HasArgument("assemblyVersion"))
            {
                var av = ctx.Argument<string>("assemblyVersion");
                var pv = ctx.Argument("packageVersion", av);
                ctx.DisableGitVersion = true;
                ctx.BuildVersion = new BuildVersion(av, pv);
            }
        });
    }
}
