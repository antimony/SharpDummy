using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using SharpDummy.Infrastructure.Domain.Interfaces;

namespace SharpDummy.Infrastructure.NHibernate.Querying
{
    public class QueryBuilder : IQueryBuilder
    {
        private readonly ISessionProvider sessionProvider;
        public QueryBuilder(ISessionProvider sessionProvider)
        {
            if (sessionProvider == null)
                throw new ArgumentNullException("sessionProvider");

            this.sessionProvider = sessionProvider;
        }

        public IRepository<TResult> For<TResult>()
        {
            return new Repository<TResult>(sessionProvider);
        }

        ///<summary>
        ///</summary>
        private class Repository<TEntity> : IRepository<TEntity>
        {
            private readonly ISessionProvider sessionProvider;

            ///<summary>
            ///</summary>
            ///<param name="sessionProvider"></param>
            /// <exception cref="ArgumentNullException"><c>sessionProvider</c> is null.</exception>
            public Repository(ISessionProvider sessionProvider)
            {
                if (sessionProvider == null)
                    throw new ArgumentNullException("sessionProvider");

                this.sessionProvider = sessionProvider;
            }

            private ISession Session
            {
                get { return sessionProvider.CurrentSession; }
            }

            #region IRepository<TEntity> Members

            public TEntity ById(int id)
            {
                return Session.Get<TEntity>(id);
            }

            public TEntity Load(int id)
            {
                return Session.Load<TEntity>(id);
            }

            public IEnumerable<TEntity> All()
            {
                return Query().ToList();
            }

            #endregion

            public IQueryable<TEntity> Query()
            {
                return sessionProvider.CurrentSession.Query<TEntity>();
            }
        }
    }

}