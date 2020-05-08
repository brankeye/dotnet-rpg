using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_rpg.Models;

namespace dotnet_rpg.Services
{
    public class ServiceResponse<T>
    {
        private ServiceResponse(T data, bool success, string message) {
          Data = data;
          Success = success;
          Message = message;
        }

        public static ServiceResponse<T> Successful(T data = default(T)) {
          var response = new ServiceResponse<T>(data, true, null);
          return response;
        }

        public static ServiceResponse<T> NotSuccessful(T data = default(T)) {
          var response = new ServiceResponse<T>(data, false, null);
          return response;
        }

        public T Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}