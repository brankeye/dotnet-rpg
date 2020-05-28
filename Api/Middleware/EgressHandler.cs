using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using dotnet_rpg.Infrastructure.Exceptions;
using System.Text.Json;
using System.Security.Authentication;
using System.IO;
using dotnet_rpg.Infrastructure.Enums;
using dotnet_rpg.Service.Exceptions;
using Microsoft.Extensions.Hosting;

namespace dotnet_rpg.Api.Middleware
{
    public class EgressHandler 
    {
        private readonly RequestDelegate _next;

        public EgressHandler(RequestDelegate next) 
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context, IWebHostEnvironment env) 
        {
            var currentBody = context.Response.Body;
            var memoryStream = new MemoryStream();
            string response = null;
            context.Response.Body = memoryStream;

            try
            {
                await _next(context);
            }
            catch (RepositoryException ex) {
                response = HandleExceptionAsync(context, env, ex);
            }
            catch (ValidationException ex) {
                response = HandleExceptionAsync(context, env, ex);
            }
            catch (AuthenticationException ex) {
                response = HandleExceptionAsync(context, env, ex);
            }
            catch (Exception ex)
            {
                response = HandleExceptionAsync(context, env, ex);
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

        private string HandleExceptionAsync(HttpContext context, IWebHostEnvironment env, Exception exception) 
        {
            var response = CreateErrorResponse(exception);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            return response;
        }

        private string HandleExceptionAsync(HttpContext context, IWebHostEnvironment env, RepositoryException exception) 
        {
            var response = CreateErrorResponse(exception, env.IsDevelopment());
            context.Response.ContentType = "application/json";

            if (exception.Status == DbStatusCode.NotFound)
            {
                context.Response.StatusCode = (int) HttpStatusCode.NotFound;
            }
            else
            {
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            }

            return response;
        }

        private string HandleExceptionAsync(HttpContext context, IWebHostEnvironment env, ValidationException exception)
        {
            var response = CreateErrorResponse(exception);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
            return response;
        }

        private string HandleExceptionAsync(HttpContext context, IWebHostEnvironment env, AuthenticationException exception) 
        {
            var response = CreateErrorResponse(exception);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
            return response;
        }

        private string CreateServerResponse(object data)
        {
            return Serialize(new ServerResponse(data));
        }

        private string CreateErrorResponse(object data) 
        {
            if (data == null) {
                return CreateErrorResponse("Something went wrong.");
            }

            var payload = new {
                data
            };
            object[] messages = { data };
            var errorMessage = new {
                errors = messages
            };

            return Serialize(errorMessage);
        }

        private string CreateErrorResponse(string message) 
        {
            var payload = new {
                message
            };
            object[] messages = { payload };
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

        private string CreateErrorResponse(ValidationException exception) 
        {
            var errorMessage = new {
                errors = exception.Errors
            };
            return Serialize(errorMessage);
        }

        private string Serialize(object value) 
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            return JsonSerializer.Serialize(value, options);
        }

        private object Deserialize(string value)
        {
            if (value == null || value == "") {
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
}