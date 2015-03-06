using System;
using System.Collections.Generic;

namespace OwinFriendlyExceptions.Plugins.WebApi2
{
    public class WebApi2ExceptionProvider : IExceptionProvider
    {
        public Exception GetException(IDictionary<string, object> environment)
        {
            object exception = null;
            environment.TryGetValue("webapi.exception", out exception);
            return (Exception) exception;
        }
    }
}