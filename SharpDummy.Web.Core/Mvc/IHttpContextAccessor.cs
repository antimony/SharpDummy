

using System.Web;

namespace SharpDummy.Web.Core.Mvc {
    public interface IHttpContextAccessor {
        HttpContextBase Current();
    }

    public class HttpContextAccessor : IHttpContextAccessor {
        public HttpContextBase Current() {
            var httpContext = HttpContext.Current;
            return httpContext == null ? null : new HttpContextWrapper(httpContext);
        }
    }
}
