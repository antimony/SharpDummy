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

    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class ControllerAuthorizeAttribute : AuthorizeAttribute
    {

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.GetCustomAttributes(true).Any(a => a is NonAuthorizedAttribute))
            {
                return;
            }
            base.OnAuthorization(filterContext);
        }

    }
}
