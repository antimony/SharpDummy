using System.Linq;
using System.Web;
using System.Web.Mvc;
using SharpDummy.Web.Core.Attributes;
using SharpDummy.Web.Core.Resources;
using SharpDummy.Web.Core.Security;
using SharpDummy.Infrastructure.Domain.Entities;
using SharpDummy.Infrastructure.Domain.Interfaces;
using SharpDummy.Infrastructure.Domain.Interfaces.UnitOfWork;
using SharpDummy.Web.Core.ViewModels;

namespace SharpDummy.Web.Core.Controllers
{
	public class AccountController:SharpDummyController
	{
		private readonly IMembershipService membership;
		public AccountController(IAuthenticationService authenticationService, IUnitOfWorkFactory unitOfWorkFactory, IQueryBuilder queryBuilder, IMembershipService membershipService)
			: base(authenticationService, unitOfWorkFactory, queryBuilder)
		{
			this.membership = membershipService;
		}

		[NonAuthorized]
		public ActionResult LogOn()
		{
			return View();
		}

		[NonAuthorized]
		public ActionResult Init()
		{
			if (QueryBuilder.For<Person>().Query().Any())
			{
				throw new HttpException(404, CoreStrings.Error404);
			}
			var model = new CreatePersonViewModel() {Email = "admin@mycompany.com", Password = "admin", Username = "admin"};
			var user = membership.CreateUser(new CreateUserParams(model, true));
			if (user == null)
			{
				ModelState.AddModelError("Authentication", CoreStrings.AuthenticationError);
				RedirectToAction("LogOn");
			}
			AuthenticationService.SignIn(user, true);
			UnitOfWork.Save(user);
			UnitOfWork.Commit();
			return RedirectToAction("LogOn");
		}

		[NonAuthorized]
		[HttpPost]
		public ActionResult LogOn(LogOnViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			var user = membership.ValidateUser(model.Username, model.Password);
			if (user == null)
			{
				ModelState.AddModelError("Authentication", CoreStrings.AuthenticationError);
				return View();
			}
			AuthenticationService.SignIn(user, true);
			return RedirectToAction("Index", "Home");
		}

	}
}
