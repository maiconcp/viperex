using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Viper.Common;

namespace Viper.Commom.Api
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var response = context.HttpContext.Response;
            response.ContentType = "application/json";

            if (context.Exception is DomainException)
            {
                response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                context.Result = new JsonResult((context.Exception as DomainException).Notifications);
            }
            else
            {
                response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;

                var notification = new Notification(string.Empty, context.Exception.Message);

                context.Result = new JsonResult(new List<Notification>() { notification });
            }
        }
    }
}
