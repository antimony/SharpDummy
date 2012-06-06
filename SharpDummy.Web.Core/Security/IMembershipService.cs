using SharpDummy.Infrastructure.Domain.Entities;

namespace SharpDummy.Web.Core.Security {
    public interface IMembershipService {
        MembershipSettings GetSettings();

        Person CreateUser(CreateUserParams createUserParams);
		Person GetUser(string username);

		Person ValidateUser(string userNameOrEmail, string password);
		void SetPassword(Person user, string password);

    }
}
