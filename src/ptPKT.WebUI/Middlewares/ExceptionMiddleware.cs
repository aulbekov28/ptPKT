using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ptPKT.Core.Exceptions.Indentity;

namespace ptPKT.WebUI.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = GetHttpStatusCode(exception);
            var result = JsonConvert.SerializeObject(new { error = exception.Message });
            return context.Response.WriteAsync(result);
        }

        private int GetHttpStatusCode(Exception exception)
        {
            HttpStatusCode httpStatusCode;

            switch (exception)
            {
                case UserNotFound userNotFoundException:
                case EmailNotConfirmedException emailNotConfirmedException:
                case IncorrectCredentialsException userIncorrectPasswordException:
                    httpStatusCode = HttpStatusCode.Unauthorized;
                    break;
                case UserIsLockedException userIsLockedExceptionException:
                    httpStatusCode = HttpStatusCode.Forbidden;
                    break;
                default:
                    httpStatusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            return (int)httpStatusCode;
        }
    }
}
