using System;
using System.Web.Security;
using SharpDummy.Web.Core.Mvc;
using Common.Logging;
using SharpDummy.Infrastructure.Domain.Entities;
using SharpDummy.Infrastructure.Domain.Interfaces;

namespace SharpDummy.Web.Core.Security {
    public class FormsAuthenticationService : IAuthenticationService
    {
        private readonly IQueryBuilder queryBuilder;
        private readonly IHttpContextAccessor httpContextAccessor;
        ILog log = LogManager.GetCurrentClassLogger();
        private Person signedInPerson;

        public FormsAuthenticationService(IQueryBuilder queryBuilder, IHttpContextAccessor httpContextAccessor)
        {
            this.queryBuilder = queryBuilder;
            this.httpContextAccessor = httpContextAccessor;
            ExpirationTimeSpan = TimeSpan.FromDays(14);
        }
        
        public TimeSpan ExpirationTimeSpan { get; set; }

		public void SignIn(Person person, bool createPersistentCookie)
		{
			var userData = Convert.ToString(person.Id);
			FormsAuthentication.SetAuthCookie(userData, createPersistentCookie);
			signedInPerson = person;
		}

    	public void SignOut() {
            signedInPerson = null;
            FormsAuthentication.SignOut();
        }

		public void SetAuthenticatedUserForRequest(Person person)
		{
			signedInPerson = person;
		}

    	public int? GetAuthenticatedUserId()
        {
            if (signedInPerson != null)
                return signedInPerson.Id;

            var httpContext = httpContextAccessor.Current();
            if (httpContext == null || !httpContext.Request.IsAuthenticated || !(httpContext.User.Identity is FormsIdentity))
            {
                return null;
            }

            var formsIdentity = (FormsIdentity)httpContext.User.Identity;
            var userData = formsIdentity.Ticket.Name;
            int userId;
            if (!int.TryParse(userData, out userId))
            {
                log.Fatal("User id not a parsable integer");
                return null;
            }
            return userId;
        }

		public Person GetAuthenticatedUser()
		{
            if (signedInPerson != null)
                return signedInPerson;

            var httpContext = httpContextAccessor.Current();
            if (httpContext == null || !httpContext.Request.IsAuthenticated || !(httpContext.User.Identity is FormsIdentity)) {
                return null;
            }

            var formsIdentity = (FormsIdentity)httpContext.User.Identity;
            var userData = formsIdentity.Ticket.Name;
            int userId;
            if (!int.TryParse(userData, out userId))
            {
                log.Fatal("User id not a parsable integer");
                return null;
            }
			return signedInPerson = queryBuilder.For<Person>().ById(userId);
        }
    }
}
