using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using dotnet_rpg.Exceptions;
using System.Text.Json;
using System.Security.Authentication;

namespace dotnet_rpg
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;

        public ExceptionHandler(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context, IWebHostEnvironment env)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException ex) {
                await HandleExceptionAsync(context, env, ex);
            }
            catch (ValidationException ex) {
                await HandleExceptionAsync(context, env, ex);
            }
            catch (AuthenticationException ex) {
                await HandleExceptionAsync(context, env, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, env, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, IWebHostEnvironment env, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            object[] messages = { new { message = exception.Message } };
            var errorMessage = new {
                errors = messages
            };
                
            string result = Serialize(errorMessage);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }

        private static Task HandleExceptionAsync(HttpContext context, IWebHostEnvironment env, NotFoundException exception)
        {
            var code = HttpStatusCode.NotFound;

            object[] messages = { new { message = exception.Message } };
            var errorMessage = new {
                errors = messages
            };
                
            string result = Serialize(errorMessage);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }

        private static Task HandleExceptionAsync(HttpContext context, IWebHostEnvironment env, ValidationException exception)
        {
            var code = HttpStatusCode.BadRequest;

            var errorMessage = new {
                errors = exception.Errors
            };
                
            string result = Serialize(errorMessage);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }

        private static Task HandleExceptionAsync(HttpContext context, IWebHostEnvironment env, AuthenticationException exception)
        {
            var code = HttpStatusCode.Unauthorized;

            object[] messages = { new { message = exception.Message } };
            var errorMessage = new {
                errors = messages
            };
                
            string result = Serialize(errorMessage);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }

        private static string Serialize(object value) {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            return JsonSerializer.Serialize(value, options);
        }
    }
}