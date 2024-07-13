using Journey.Communication.Responses;
using Journey.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Journey.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is JorneyException)
            {
                var jorneyException = (JorneyException)context.Exception;
                context.HttpContext.Response.StatusCode = (int)jorneyException.GetStatusCode() ;

                var responseJson = new ResponseErrosJson(jorneyException.GetErrorMessages());

                context.Result = new NotFoundObjectResult(responseJson);
            }
            else
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var responseJson = new ResponseErrosJson(new List<string> { "Error Desconhecido" });

                context.Result = new ObjectResult(responseJson);
            }
        }
    }
}
