using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using SharpDummy.Infrastructure.Domain.Entities;
using SharpDummy.Infrastructure.Domain.Interfaces;
using SharpDummy.Infrastructure.Domain.Interfaces.UnitOfWork;

namespace SharpDummy.Web.Core.Security {
    public class MembershipService : IMembershipService {
        private readonly IQueryBuilder queryBuilder;

        public MembershipService(IQueryBuilder queryBuilder, IUnitOfWorkFactory unitOfWorkFactory)
        {
            this.queryBuilder = queryBuilder;
        }

        public MembershipSettings GetSettings() {
            var settings = new MembershipSettings();
            return settings;
        }

		public Person CreateUser(CreateUserParams createUserParams)
        {
			var user = new Person
                           {
                               Username = createUserParams.Username.ToLowerInvariant(),
                               Email = createUserParams.Email
                           };
            SetPassword(user, createUserParams.Password);
            return user;
        }

		public Person GetUser(string username)
        {
            var lowerName = username == null ? "" : username.ToLowerInvariant();
			return queryBuilder.For<Person>().Query().FirstOrDefault(p => p.Username == lowerName);
        }

		public Person ValidateUser(string userNameOrEmail, string password)
        {
            var lowerName = userNameOrEmail == null ? "" : userNameOrEmail.ToLowerInvariant();
			var user = queryBuilder.For<Person>().Query().FirstOrDefault(p => p.Username == lowerName);
            if ( user == null || ValidatePassword(user, password) == false )
                return null;
            return user;
        }

		public void SetPassword(Person partRecord, string password)
        {
            SetPasswordHashed(partRecord, password);
        }

		private bool ValidatePassword(Person partRecord, string password)
        {
            return ValidatePasswordHashed(partRecord, password);
        }

		private static void SetPasswordHashed(Person partRecord, string password)
		{

            var saltBytes = new byte[0x10];
            using (var random = new RNGCryptoServiceProvider()) {
                random.GetBytes(saltBytes);
            }

            var passwordBytes = Encoding.Unicode.GetBytes(password);

            var combinedBytes = saltBytes.Concat(passwordBytes).ToArray();

            byte[] hashBytes;
            using (var hashAlgorithm = MD5.Create()) {
                hashBytes = hashAlgorithm.ComputeHash(combinedBytes);
            }
            partRecord.Password = Convert.ToBase64String(hashBytes);
            partRecord.PasswordSalt = Convert.ToBase64String(saltBytes);
        }

		private static bool ValidatePasswordHashed(Person partRecord, string password)
        {

            var saltBytes = Convert.FromBase64String(partRecord.PasswordSalt);

            var passwordBytes = Encoding.Unicode.GetBytes(password);

            var combinedBytes = saltBytes.Concat(passwordBytes).ToArray();

            byte[] hashBytes;
            using (var hashAlgorithm = MD5.Create()) {
                hashBytes = hashAlgorithm.ComputeHash(combinedBytes);
            }
            return partRecord.Password == Convert.ToBase64String(hashBytes);
        }
    }
}
