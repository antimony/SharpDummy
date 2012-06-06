using System.Data;
using SharpDummy.Infrastructure.Domain.Interfaces.UnitOfWork;

namespace SharpDummy.Infrastructure.NHibernate.UnitOfWorkAware
{
    public class NHibernateUnitOfWorkFactory : IUnitOfWorkFactory
    {
        #region IUnitOfWorkFactory Members


        /// <summary>
        /// Creates nhibernate Unit of work with isolationLevel
        /// </summary>
        public IUnitOfWork Create(IsolationLevel isolationLevel)
        {
            return new NHibernateUnitOfWork(NHibernateHelper.SessionFactory.OpenSession(), isolationLevel);
        }

        /// <summary>
        /// Creates nhibernate Unit of work
        /// </summary>
        public IUnitOfWork Create()
        {
            return Create(IsolationLevel.ReadCommitted);
        }

        #endregion
    }
}