using Cake.Core;

namespace DefiantCode.Cake.Frosting
{
    public interface ILifetimeActions
    {
        void BeforeSetup(DotNetCoreContext context);
        void AfterSetup(DotNetCoreContext context);
        void BeforeTeardown(DotNetCoreContext context, ITeardownContext info);
        void AfterTeardown(DotNetCoreContext context, ITeardownContext info);
    }
}