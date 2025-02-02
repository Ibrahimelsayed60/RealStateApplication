using Microsoft.AspNetCore.Mvc;
using RealState.Presentation.Errors;
using System.Net;
using System.Text.Json;

namespace RealState.Presentation.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
                    
            }
            catch (UnauthorizedAccessException)
            {
                httpContext.Response.Redirect("/Errors/Unauthorization");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                httpContext.Response.StatusCode = 500;
                await httpContext.Response.WriteAsync("An error occurred.");
            }
        }

    }
}
