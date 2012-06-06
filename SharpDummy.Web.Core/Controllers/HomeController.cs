using System.Web.Mvc;
using SharpDummy.Web.Core.Attributes;
using SharpDummy.Web.Core.Security;
using SharpDummy.Infrastructure.Domain.Interfaces;
using SharpDummy.Infrastructure.Domain.Interfaces.UnitOfWork;

namespace SharpDummy.Web.Core.Controllers
{
	public class HomeController:SharpDummyController
	{
		public HomeController(IAuthenticationService authenticationService, IUnitOfWorkFactory unitOfWorkFactory, IQueryBuilder queryBuilder) : base(authenticationService, unitOfWorkFactory, queryBuilder)
		{
		}
		[NonAuthorized]
		public ActionResult Index()
		{
			return View();
		}
	}
}
