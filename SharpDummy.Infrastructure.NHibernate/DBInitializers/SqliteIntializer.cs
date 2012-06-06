using System.IO;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;
using SharpDummy.Infrastructure.NHibernate.Mappings;

namespace SharpDummy.Infrastructure.NHibernate.DBInitializers
{
    public class SqliteIntializer:INHibernateInitializer
    {
        public ISessionFactory GetSessionFactory()
        {
            var persistenceConfigurer = SQLiteConfiguration.Standard.ConnectionString(connectionStringBuilder => connectionStringBuilder.FromConnectionStringWithKey("test")).ShowSql();
            return Fluently.Configure()
                    .Database(persistenceConfigurer)
                    .CurrentSessionContext<ThreadStaticSessionContext>()
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<PersonMap>())
                    .ExposeConfiguration(BuildSchema)
                    .BuildSessionFactory();
        }

        private static void BuildSchema(Configuration config)
        {
            // delete the existing db on each run
            if (File.Exists("C:\\Temp\\test.db"))
                File.Delete("C:\\Temp\\test.db");

            // this NHibernate tool takes a configuration (with mapping info in)
            // and exports a database schema from it
            new SchemaExport(config)
              .Create(false, true);
        }
    }
}
