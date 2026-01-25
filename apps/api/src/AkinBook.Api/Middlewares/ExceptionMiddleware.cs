using AkinBook.Api.Common;
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
            catch(Exception ex)
            {
                context.Response.ContentType = "application/json";

                var (statusCode, type, message, details) = ex switch
                {
                    UnauthorizedAccessException => ((int)HttpStatusCode.Unauthorized, "unauthorized", "Unauthorized.", null),
                    InvalidOperationException ioe => ((int)HttpStatusCode.BadRequest, "bad_request", ioe.Message, null),
                    KeyNotFoundException knf => ((int)HttpStatusCode.NotFound, "not_found", knf.Message, null),
                    _ => ((int)HttpStatusCode.InternalServerError, "server_error", "Unexpected error.", new { ex.Message })
                };

                context.Response.StatusCode = statusCode;

                var payload = new ApiError
                {
                    Type = type,
                    Message = message,
                    Details = details,
                    TraceId = context.TraceIdentifier
                };

                var json = JsonSerializer.Serialize(payload, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                });

                await context.Response.WriteAsync(json);
            }
        }
    }
}
