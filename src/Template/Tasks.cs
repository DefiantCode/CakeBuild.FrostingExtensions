using Cake.Frosting;
using DefiantCode.Cake.Frosting;
using DefiantCode.Cake.Frosting.Tasks;

[TaskName("Default")]
public class DefaultTask : FrostingTask<DotNetCoreContext>
{
    public override void Run(DotNetCoreContext context)
    {
    }
}

[TaskName("build")]
[Dependency(typeof(DotNetCoreBuild))]
public class BuildTask : FrostingTask<DotNetCoreContext>
{
    public override void Run(DotNetCoreContext context)
    {
    }
}

[TaskName("clean")]
[Dependency(typeof(DotNetCoreClean))]
public class CleanTask : FrostingTask<DotNetCoreContext>
{
    public override void Run(DotNetCoreContext context)
    {
    }
}

[TaskName("restore")]
[Dependency(typeof(DotNetCoreRestore))]
public class RestoreTask : FrostingTask<DotNetCoreContext>
{
    public override void Run(DotNetCoreContext context)
    {
    }
}

[TaskName("pack")]
[Dependency(typeof(DotNetCorePack))]
public class PackTask : FrostingTask<DotNetCoreContext>
{
    public override void Run(DotNetCoreContext context)
    {
    }
}

[TaskName("publish")]
[Dependency(typeof(DotNetCorePublish))]
public class PublishTask : FrostingTask<DotNetCoreContext>
{
    public override void Run(DotNetCoreContext context)
    {
    }
}