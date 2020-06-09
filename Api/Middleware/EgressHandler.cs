using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Text.Json;
using System.IO;
using dotnet_rpg.Service.Utility.ExceptionUtility;

namespace dotnet_rpg.Api.Middleware
{
    public class EgressHandler : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var currentBody = context.Response.Body;
            var memoryStream = new MemoryStream();
            string response = null;
            context.Response.Body = memoryStream;

            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                response = HandleExceptionAsync(context, ex);
            }

            context.Response.Body = currentBody;
            memoryStream.Seek(0, SeekOrigin.Begin);

            var streamReader = new StreamReader(memoryStream);
            var streamResult = await streamReader.ReadToEndAsync();
            
            if (context.Response.StatusCode >= 300) {
                response ??= CreateErrorResponse(Deserialize(streamResult));
                await context.Response.WriteAsync(response);
            } else {
                response ??= CreateServerResponse(Deserialize(streamResult));
                await context.Response.WriteAsync(response);
            }
        }

        private static string HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errors = ExceptionUtility.GetErrors(exception);
            var response = CreateErrorResponse(errors);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            return response;
        }

        private static string CreateServerResponse(object data)
        {
            return Serialize(new ServerResponse(data));
        }

        private static string CreateErrorResponse(object data) 
        {
            if (data == null) {
                var errors = ExceptionUtility.GetErrors();
                var errorResponse = new ErrorResponse(errors);
                return Serialize(errorResponse);
            }

            object[] messages = { data };
            var errorMessage = new {
                errors = messages
            };

            return Serialize(errorMessage);
        }

        private string CreateErrorResponse(Exception exception, bool isDevelopment = false)
        {
            object payload = null;

            if (isDevelopment)
            {
                payload = new {
                    message = exception.Message,
                    stackTrace = exception.ToString()
                };
            }
            else
            {
                payload = new {
                    message = exception.Message
                };
            }
            
            object[] messages = { payload };
            var errorMessage = new {
                errors = messages
            };
            return Serialize(errorMessage);
        }

        private static string Serialize(object value) 
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            return JsonSerializer.Serialize(value, options);
        }

        private static object Deserialize(string value)
        {
            if (string.IsNullOrEmpty(value)) {
                return null;
            }
 
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            return JsonSerializer.Deserialize<object>(value, options);
        }
    }

    public class ServerResponse 
    {
        public ServerResponse(object data)
        {
            Data = data;
        }

        public object Data { get; set; }
    }

    public class ErrorResponse
    {
        public ErrorResponse(IEnumerable<ServiceError> errors)
        {
            Errors = errors;
        }

        public IEnumerable<object> Errors { get; set; }
    }
}