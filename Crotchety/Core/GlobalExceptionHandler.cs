using System;
using System.Net;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Crotchety.Core
{
	public class GlobalExceptionHandler : IMiddleware
	{
        private readonly ILogger _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) => _logger = logger;
        
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        { 
            try
            {
                await next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                switch (error)
                {
                    case AppException e:
                        // custom application/validation error 400
                        response.StatusCode = (int) HttpStatusCode.BadRequest;
                        _logger.LogError(e.Message);
                        break;
                    case KeyNotFoundException e:
                        // not found error 404
                        response.StatusCode = (int) HttpStatusCode.NotFound;
                        _logger.LogError(e.Message);
                        break;
                    default:
                        // unhandled error 500
                        response.StatusCode = (int) HttpStatusCode.InternalServerError;
                        _logger.LogCritical(error.Message);
                        break;
                }

                var result = Newtonsoft.Json.JsonConvert.SerializeObject(new { message = error?.Message }, Formatting.Indented);
                await response.WriteAsync(result);
            }
        }

    }
}

