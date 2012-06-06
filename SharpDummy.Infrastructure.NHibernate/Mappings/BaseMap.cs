using FluentNHibernate.Mapping;
using SharpDummy.Infrastructure.Domain.Interfaces;

namespace SharpDummy.Infrastructure.NHibernate.Mappings
{
    public class BaseMap<TEntity> : ClassMap<TEntity>
        where TEntity : class, IId<int>
    {
        protected BaseMap()
        {
            Id(x => x.Id);
        }
    }
}