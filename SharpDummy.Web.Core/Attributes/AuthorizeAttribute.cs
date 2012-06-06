using System;
using System.Linq;
using System.Web.Mvc;

namespace SharpDummy.Web.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class NonAuthorizedAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
        }
    }

    // New attribute that is only usable at the class (controller) level
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class ControllerAuthorizeAttribute : AuthorizeAttribute
    {

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //If the target action has been decorated with an ActionAuthorizeAttribute, skip controller auth
            if (filterContext.ActionDescriptor.GetCustomAttributes(true).Any(a => a is NonAuthorizedAttribute))
            {
                return;
            }
            base.OnAuthorization(filterContext);
        }

    }
}
