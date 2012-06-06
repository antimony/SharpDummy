using NHibernate;

namespace SharpDummy.Infrastructure.NHibernate
{
    ///<summary>
    /// Bootstrapper for nhibernate
    ///</summary>
    public interface INHibernateInitializer
    {
        ///<summary>
        /// Builds and returns nhibernate session factory
        ///</summary>
        ///<returns>NHibernate session factory object</returns>
        ISessionFactory GetSessionFactory();
    }
}