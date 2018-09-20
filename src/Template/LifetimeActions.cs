using Cake.Core;
using DefiantCode.Cake.Frosting;

public class LifetimeActions : ILifetimeActions
{
    public void AfterSetup(DotNetCoreContext context)
    {
    }

    public void AfterTeardown(DotNetCoreContext context, ITeardownContext info)
    {
    }

    public void BeforeSetup(DotNetCoreContext context)
    {
        //Disables git version and adds assemblyVersion and packageVersion arguments
        //if (context.HasArgument("assemblyVersion"))
        //{
        //    var av = context.Argument<string>("assemblyVersion");
        //    var pv = context.Argument("packageVersion", av);
        //    context.DisableGitVersion = true;
        //    context.BuildVersion = new BuildVersion(av, pv);
        //}
    }

    public void BeforeTeardown(DotNetCoreContext context, ITeardownContext info)
    {
    }
}
