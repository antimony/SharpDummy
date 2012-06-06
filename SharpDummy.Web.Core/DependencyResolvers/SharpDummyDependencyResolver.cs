using System;
using System.Collections.Generic;
using System.Web.Mvc;
using SharpDummy.Web.Core.Mvc;
using SharpDummy.Web.Core.Security;
using Ninject.Modules;
using SharpDummy.Infrastructure.Common.DependencyResolver;
using SharpDummy.Infrastructure.Domain.Interfaces;
using SharpDummy.Infrastructure.Domain.Interfaces.UnitOfWork;
using SharpDummy.Infrastructure.NHibernate;
using SharpDummy.Infrastructure.NHibernate.DBInitializers;
using SharpDummy.Infrastructure.NHibernate.Querying;
using SharpDummy.Infrastructure.NHibernate.UnitOfWorkAware;

namespace SharpDummy.Web.Core.DependencyResolvers
{
	public class SharpDummyDependencyResolver : IDependencyResolver
	{
		public SharpDummyDependencyResolver()
		{
			IoC.Initialize(new INinjectModule[]
                               {
                                   new DBModule(), new MvcModule(), 
                               });
		}

		public object GetService(Type serviceType)
		{
			return IoC.Resolve(serviceType);
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return IoC.ResolveAll(serviceType);
		}
	}

	class DBModule : NinjectModule
	{
		public override void Load()
		{
			Bind<ISessionProvider>().To<SessionProvider>();
			Bind<IQueryBuilder>().To<QueryBuilder>().When(x => x.Target == null || x.Target.Name == "queryBuilder");
			Bind<IQueryBuilder>().To<QueryBuilder>().When(x => x.Target != null && x.Target.Name == "personQueryBuilder");
			Bind<IUnitOfWorkFactory>().To<NHibernateUnitOfWorkFactory>();
			Bind<INHibernateInitializer>().To<MSSqlIntializer>();
		}
	}

	class MvcModule : NinjectModule
	{
		public override void Load()
		{
			Bind<IControllerFactory>().To<DefaultControllerFactory>();
			Bind<IMembershipService>().To<MembershipService>();
			Bind<IHttpContextAccessor>().To<HttpContextAccessor>();
			Bind<IAuthenticationService>().To<FormsAuthenticationService>();
		}
	}
}
