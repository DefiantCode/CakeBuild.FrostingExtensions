using Cake.Common;
using Cake.Common.Build;
using Cake.Frosting;
using System;
using Cake.Core;
using System.Collections.Generic;
using System.Linq;

namespace DefiantCode.Cake.Frosting
{
    public class DynamicLifetime : FrostingLifetime<DynamicContext>
    {
        private static object _lock = new object();
        private static Action<DynamicContext> _setupAction = null;
        private static Action<DynamicContext> _teardownAction = null;

        public static void RegisterAdditionalSetup(Action<DynamicContext> setupAction)
        {
            lock(_lock)
                _setupAction = setupAction;
        }

        public static void RegisterAdditionalTeardown(Action<DynamicContext> teardownAction)
        {
            lock (_lock)
                _teardownAction = teardownAction;
        }

        public override void Setup(DynamicContext context)
        {
            context.Target = context.Argument("target", "Default");
            var buildSystem = context.BuildSystem();
            context.IsLocalBuild = buildSystem.IsLocalBuild;

            var dynProps = (IDictionary<string, object>)context.Properties;

            //Get the cli args and add them as properties
            GetAllArguments().ForEach(dynProps.Add);

            lock(_lock)
                _setupAction?.Invoke(context);
        }

        public override void Teardown(DynamicContext context, ITeardownContext info)
        {
            lock (_lock)
                _teardownAction?.Invoke(context);
        }
        private static string GetEnvOrArg(DotNetCoreContext context, string environmentVariable, string argumentName)
        {
            var arg = context.EnvironmentVariable(environmentVariable);
            if (string.IsNullOrWhiteSpace(arg))
            {
                arg = context.Argument<string>(argumentName, null);
            }
            return arg;
        }

        private static List<KeyValuePair<string, object>> GetAllArguments()
        {
            var kvpList = new List<KeyValuePair<string, object>>();
            var rawArgs = Environment.GetCommandLineArgs();
            //always skip the first arg since it is always the name of the executing binary
            for (int i = 1; i < rawArgs.Length; i++)
            {
                //strip the -- from the arg name
                var kvp = rawArgs[i].Split('=').Select(x => x.StartsWith("--") ? x.Remove(0, 2) : x).ToArray();
                kvpList.Add(new KeyValuePair<string, object>(kvp[0], kvp[1]));
            }

            return kvpList;
        }
    }
}
