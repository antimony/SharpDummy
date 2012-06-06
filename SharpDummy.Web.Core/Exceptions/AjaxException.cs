
using System;

namespace BubbleWell.Web.Core.Exceptions
{
    public class AjaxException : Exception
    {
        private readonly string message;
        public override string Message
        {
            get
            {
                return message;
            }
        }

        public AjaxException(string message)
        {
            this.message = message;
        }
    }
}
