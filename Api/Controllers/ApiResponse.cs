using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Api.Controllers
{
    public class ApiResponse : IActionResult
    {
        protected HttpStatusCode StatusCode { get; }

        protected ApiResponse(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public static ApiResponse Ok()
        {
            return new ApiResponse(HttpStatusCode.OK);
        }
        
        public static ApiResponse<T> Ok<T>(T data)
        {
            return new ApiResponse<T>(HttpStatusCode.OK, data);
        }
        
        public static ApiResponse<T> Created<T>(string location, T data)
        {
            return new ApiResponse<T>(HttpStatusCode.Created, location, data);
        } 
        
        public virtual async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new StatusCodeResult((int) StatusCode);
            await objectResult.ExecuteResultAsync(context);
        }
    }
    
    public class ApiResponse<T> : ApiResponse
    {
        private T Data { get; }
        
        private string Location { get; }
        
        public ApiResponse(HttpStatusCode statusCode, T data) : base(statusCode)
        {
            Data = data;
        }
        
        public ApiResponse(HttpStatusCode statusCode, string location, T data) : base(statusCode)
        {
            Location = location;
            Data = data;
        }

        public override async Task ExecuteResultAsync(ActionContext context)
        {
            if (Location != null)
            {
                var headers = context?.HttpContext?.Response?.Headers;
                headers?.Add("Location", Location);
            }
            
            var objectResult = new ObjectResult(Data)
            {
                StatusCode = (int) StatusCode,
            };
            
            await objectResult.ExecuteResultAsync(context);
        }
    }
}