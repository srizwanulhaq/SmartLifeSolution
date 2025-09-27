using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SmartLifeSolution.DAL.Dao;
using SmartLifeSolution.DAL.Dao.GenericResponse;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace SmartLifeSolution.BLL.Middleware
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomMiddleware> _logger;

        public CustomMiddleware(RequestDelegate next, ILogger<CustomMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = exception switch
            {
                ArgumentNullException => HttpStatusCode.BadRequest,
                UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                KeyNotFoundException => HttpStatusCode.NotFound,
                _ => HttpStatusCode.InternalServerError
            };

            var response = new GenericResponseDao
            {
                StatusCode = (int)statusCode,
                Message = exception.Message,
                IsError = true
              //  Details = exception.StackTrace // Remove this in production for security
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
