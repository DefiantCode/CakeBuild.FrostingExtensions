using Cake.Core;
using Cake.Core.IO;
using Cake.Frosting;
using Cake.Incubator;
using DefiantCode.Cake.Frosting.Utilities;
using System;
using System.Collections.Generic;

namespace DefiantCode.Cake.Frosting
{
    [Obsolete("Use the class DotNetCoreContext")]
    public class Context : DotNetCoreContext
    {

        public Context(ICakeContext context) : base(context)
        {
          
        }

        
    }
}