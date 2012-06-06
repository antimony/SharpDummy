using NHibernate;

namespace SharpDummy.Infrastructure.NHibernate
{
    ///<summary>
    ///</summary>
    public interface ISessionProvider
    {
        ///<summary>
        ///</summary>
        ///<returns></returns>
        ISession CurrentSession { get; }
    }
}