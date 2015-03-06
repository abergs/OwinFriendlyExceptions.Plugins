using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Microsoft.Owin;

namespace OwinFriendlyExceptions.Plugins.WebApi2
{
    public class WebApi2ExceptionHandler : IExceptionHandler
    {
        private readonly ITransformsCollection _transforms;

        public WebApi2ExceptionHandler(ITransformsCollection transforms)
        {
            _transforms = transforms;
        }

        public Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            Handle(context, _transforms);
            return Task.FromResult(0);
        }

        private static void Handle(ExceptionHandlerContext context, ITransformsCollection transforms)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            ExceptionContext exceptionContext = context.ExceptionContext;

            HttpRequestMessage request = exceptionContext.Request;

            if (request == null)
            {
                throw new ArgumentException("request");
            }

            if (exceptionContext.CatchBlock == ExceptionCatchBlocks.IExceptionFilter)
            {
                // The exception filter stage propagates unhandled exceptions by default (when no filter handles the
                // exception).
                return;
            }
            
            IOwinContext owinContext = context.Request.GetOwinContext();
            owinContext.Set("webapi.exception", context.Exception);

            if (transforms.FindTransform(context.Exception) != null) { 
                context.Result = new EmptyResponse();
            }
        }

        private class EmptyResponse : IHttpActionResult
        {
            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                var res = new HttpResponseMessage();
                return Task.FromResult(res);
            }
        }
    }
}