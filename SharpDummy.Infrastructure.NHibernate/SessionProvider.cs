using NHibernate;
using SharpDummy.Infrastructure.NHibernate.UnitOfWorkAware;

namespace SharpDummy.Infrastructure.NHibernate
{
    ///<summary>
    ///</summary>
    public class SessionProvider : ISessionProvider
    {
        #region ISessionProvider Members

        public ISession CurrentSession
        {
            get { return NHibernateHelper.GetSession(); }
        }

        #endregion
    }
}