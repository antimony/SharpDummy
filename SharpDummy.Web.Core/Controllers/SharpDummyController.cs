using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BubbleWell.Web.Core.Exceptions;
using SharpDummy.Web.Core.Attributes;
using SharpDummy.Web.Core.Mvc;
using SharpDummy.Web.Core.Resources;
using SharpDummy.Web.Core.Security;
using Common.Logging;
using SharpDummy.Infrastructure.Domain.Entities;
using SharpDummy.Infrastructure.Domain.Interfaces;
using SharpDummy.Infrastructure.Domain.Interfaces.UnitOfWork;
using SharpDummy.Web.Core.ViewModels;

namespace SharpDummy.Web.Core.Controllers
{
	public abstract class SharpDummyController : Controller
	{
		protected readonly IAuthenticationService AuthenticationService;
		protected readonly IQueryBuilder QueryBuilder;
		private readonly IUnitOfWorkFactory unitOfWorkFactory;
		protected IUnitOfWork UnitOfWork;
		public int? CurrentPersonId
		{
			get { return AuthenticationService.GetAuthenticatedUserId(); }
		}

		public Person CurrentPerson
		{
			get { return AuthenticationService.GetAuthenticatedUser(); }
		}

		protected SharpDummyController(IAuthenticationService authenticationService, IUnitOfWorkFactory unitOfWorkFactory, IQueryBuilder queryBuilder)
		{
			AuthenticationService = authenticationService;
			this.unitOfWorkFactory = unitOfWorkFactory;
			QueryBuilder = queryBuilder;
			UnitOfWork = unitOfWorkFactory.Create();
		}

		protected override void OnAuthorization(AuthorizationContext filterContext)
		{
			if (filterContext.ActionDescriptor.GetCustomAttributes(true).Any(a => a is NonAuthorizedAttribute))
			{
				return;
			}
			base.OnAuthorization(filterContext);
			if (CurrentPerson == null)
			{
				filterContext.Result = RedirectToAction("LogOn", "Account", new { ReturnUrl = Url.Action(filterContext.RouteData.Values["action"].ToString(), filterContext.RouteData.Values["controller"].ToString()) });
			}
		}

		protected override void OnException(ExceptionContext filterContext)
		{
			ILog log = LogManager.GetCurrentClassLogger();
			log.ErrorFormat("Error in request: \n {0} \n {1}", filterContext.Exception.Message, filterContext.Exception.StackTrace);
			if (filterContext.HttpContext.Request.IsAjaxRequest())
			{
				filterContext.ExceptionHandled = true;
				if (filterContext.Exception is HttpException)
				{
					filterContext.Result = new JsonNetResult { Data = new { ErrorMessage = filterContext.Exception.Message } };
					filterContext.HttpContext.Response.StatusCode = ((HttpException)filterContext.Exception).GetHttpCode();
				}
				else
				{
					if (filterContext.Exception is AjaxException)
					{
						filterContext.Result = new JsonNetResult { Data = new { ErrorMessage = filterContext.Exception.Message } };
					}
					else
					{
						filterContext.Result = new JsonNetResult { Data = new { ErrorMessage = CoreStrings.RequestError } };
					}
					filterContext.HttpContext.Response.StatusCode = 500;
				}
			}
			else
			{
				base.OnException(filterContext);
			}
		}


		protected override void Execute(RequestContext requestContext)
		{
			using (UnitOfWork = unitOfWorkFactory.Create())
			{
				ViewBag.CurrentPerson = CurrentPerson == null ? null : new PersonViewModel(CurrentPerson);
				base.Execute(requestContext);
			}
		}

		protected T EntityById<T>(int id) where T : class, IId<int>
		{
			var entity = QueryBuilder.For<T>().ById(id);
			if (entity == null)
			{
				throw new HttpException(404, CoreStrings.Error404);
			}
			return entity;
		}
	}
}
