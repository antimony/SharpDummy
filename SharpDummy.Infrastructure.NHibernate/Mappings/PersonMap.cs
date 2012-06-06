using SharpDummy.Infrastructure.Domain.Entities;

namespace SharpDummy.Infrastructure.NHibernate.Mappings
{
    /// <summary>
    /// NHibernate person mapping
    /// </summary>
    public class PersonMap : BaseMap<Person>
    {
        public PersonMap()
        {
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.Username);
            Map(x => x.Email);
            Map(x => x.Password);
            Map(x => x.PasswordSalt);
        }
    }
}
