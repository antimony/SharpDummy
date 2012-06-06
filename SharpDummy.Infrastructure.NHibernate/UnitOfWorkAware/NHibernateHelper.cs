using System;
using NHibernate;
using NHibernate.Context;
using SharpDummy.Infrastructure.Common.DependencyResolver;

namespace SharpDummy.Infrastructure.NHibernate.UnitOfWorkAware
{
    internal static class NHibernateHelper
    {
        private static readonly object lockObject = new object();
        private static ISessionFactory sessionFactory;

        public static ISessionFactory SessionFactory
        {
            get
            {
                if (sessionFactory == null)
                {
                    lock (lockObject)
                    {
                        if (sessionFactory == null)
                            sessionFactory = IoC.Resolve<INHibernateInitializer>().GetSessionFactory();
                    }
                }

                return sessionFactory;
            }
        }
        
        public static ISession GetSession()
        {
            if (CurrentSessionContext.HasBind(SessionFactory))
                return SessionFactory.GetCurrentSession();

            throw new InvalidOperationException("Database access logic cannot be used, if session not opened. Implicitly session usage not allowed now. Please open session explicitly through UnitOfWorkFactory.StartLongConversation method");
        }
    }
}