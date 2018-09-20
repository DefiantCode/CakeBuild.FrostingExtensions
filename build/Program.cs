using Cake.Common;
using Cake.Core;
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
        DotNetCoreLifetime.RegisterActions(new LifetimeActions());
    }
}

class LifetimeActions : ILifetimeActions
{
    public void AfterSetup(DotNetCoreContext context)
    {
    }

    public void AfterTeardown(DotNetCoreContext context, ITeardownContext info)
    {
    }

    public void BeforeSetup(DotNetCoreContext context)
    {
        if (context.HasArgument("assemblyVersion"))
        {
            var av = context.Argument<string>("assemblyVersion");
            var pv = context.Argument("packageVersion", av);
            context.DisableGitVersion = true;
            context.BuildVersion = new BuildVersion(av, pv);
        }
    }

    public void BeforeTeardown(DotNetCoreContext context, ITeardownContext info)
    {
    }
}