using SharpDummy.Infrastructure.Domain.Interfaces;

namespace SharpDummy.Infrastructure.Domain.Entities
{
    public class Person : IId<int>
    {
        public virtual int Id { get; set; }
		public virtual string FirstName { get; set; }
		public virtual string LastName { get; set; }
		public virtual string Username { get; set; }
		public virtual string Email { get; set; }
		public virtual string Password { get; set; }
		public virtual string PasswordSalt { get; set; }
    }
}
