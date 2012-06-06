using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using NHibernate.Context;
using SharpDummy.Infrastructure.NHibernate.Mappings;

namespace SharpDummy.Infrastructure.NHibernate.DBInitializers
{
    public class MSSqlIntializer:INHibernateInitializer
    {
        public ISessionFactory GetSessionFactory()
        {
            var persistenceConfigurer = MsSqlConfiguration.MsSql2008
                .ConnectionString(connectionStringBuilder => connectionStringBuilder.FromConnectionStringWithKey("Sql"));
            return Fluently.Configure()
                    .Database(persistenceConfigurer)
                    .CurrentSessionContext<ThreadStaticSessionContext>() 
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<PersonMap>()
                        .Conventions.Add(
                        DefaultLazy.Always(),
                        ForeignKey.EndsWith("Id")))
                    .BuildSessionFactory();
        }
    }
}
