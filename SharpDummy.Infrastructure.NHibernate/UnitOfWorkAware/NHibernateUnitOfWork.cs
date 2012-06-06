using System;
using System.Data;
using NHibernate;
using NHibernate.Context;
using SharpDummy.Infrastructure.Domain.Interfaces.UnitOfWork;

namespace SharpDummy.Infrastructure.NHibernate.UnitOfWorkAware
{
    internal class NHibernateUnitOfWork : IUnitOfWork
    {
        private readonly ISession session;
        private readonly ISession prevSession;
        
        private ITransaction transaction;


        public NHibernateUnitOfWork(ISession session, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            if (session == null)
                throw new ArgumentNullException("session");
            if (CurrentSessionContext.HasBind(NHibernateHelper.SessionFactory))
            {
                prevSession = NHibernateHelper.SessionFactory.GetCurrentSession();    
            }
            CurrentSessionContext.Bind(session);

            this.session = session;
            transaction = session.BeginTransaction(isolationLevel);
        }

        #region IUnitOfWork Members

        public void Dispose()
        {
            if (transaction != null)
            {
                if (!transaction.WasCommitted && !transaction.WasRolledBack)
                {
                    transaction.Rollback();
                }
                transaction.Dispose();
                transaction = null;
            }
            CurrentSessionContext.Unbind(session.SessionFactory);
            if(prevSession!=null)
            {
                CurrentSessionContext.Bind(prevSession);
            }
            session.Dispose();
        }

        public void Commit()
        {
            transaction.Commit();
        }
        
        public virtual void Save(object entity)
        {
            session.SaveOrUpdate(entity);
        }

        public virtual void Delete(object entity)
        {
            session.Delete(entity);
        }

        public void ExecuteCommand(string command)
        {
            var query = session.CreateSQLQuery(command);
            query.ExecuteUpdate();
        }

        public virtual void SaveAll(object[] entity)
        {
            for (int i = 0; i < entity.Length; i++)
            {
                Save(entity[i]);
                if (i%20 == 0)
                {
                    session.Flush();
                    session.Clear();
                }
            }
        }
        #endregion
    }
}