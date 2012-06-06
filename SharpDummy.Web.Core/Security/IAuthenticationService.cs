using SharpDummy.Infrastructure.Domain.Entities;

namespace SharpDummy.Web.Core.Security {
    public interface IAuthenticationService
    {
		void SignIn(Person person, bool createPersistentCookie);
        void SignOut();
		void SetAuthenticatedUserForRequest(Person person);
		Person GetAuthenticatedUser();
        int? GetAuthenticatedUserId();
    }
}
