using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using Ninject.Modules;
using Ninject.Parameters;

namespace SharpDummy.Infrastructure.Common.DependencyResolver
{
    /// <summary>
    /// Contains links for service contract and realization.
    /// </summary>
    public static class IoC
    {
        static IKernel kernel = new StandardKernel();

        public static IKernel Kernel()
        {
            return kernel;
        }

        public static object Resolve(Type t)
        {
            var request = kernel.CreateRequest(t, null, new Parameter[0], true, true);
            return kernel.Resolve(request).FirstOrDefault();
        }

        public static T Resolve<T>()
        {
            return kernel.Get<T>();
        }
        public static IEnumerable<object> ResolveAll(Type t)
        {
            return kernel.GetAll(t).ToList();
        }
        public static T Resolve<T>(string Name)
        {
            return kernel.Get<T>(Name);
        }

        public static T ResolveWithParameters<T>(IParameter[] parameters)
        {
            return kernel.Get<T>(parameters);
        }

        public static T ResolveNamedWithParameter<T>(string name, IParameter[] parameters)
        {
            return kernel.Get<T>(name, parameters) ;
        }

        public static void Initialize(INinjectModule[] modules)
        {
            kernel = new StandardKernel(modules);
        }
    }
}