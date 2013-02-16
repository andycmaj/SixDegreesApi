using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace SixDegrees.Web.Configuration.Dependencies
{
        public class NinjectApiDependencyResolver : NinjectDependencyScope, IDependencyResolver
        {
            private IKernel kernel;

            public NinjectApiDependencyResolver(IKernel kernel)
                : base(kernel)
            {
                this.kernel = kernel;
            }

            public IDependencyScope BeginScope()
            {
                return new NinjectDependencyScope(kernel.BeginBlock());
            }
        }
}