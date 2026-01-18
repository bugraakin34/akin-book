using System.Net;
using System.Text.Json;

namespace AkinBook.Api.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch(InvalidOperationException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";

                var payload = JsonSerializer.Serialize(new {message = ex.Message});
                await context.Response.WriteAsync(payload);
            }
        }
    }
}
