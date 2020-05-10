using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_rpg.Models;

namespace dotnet_rpg.Services
{
    public class ServiceResponse<T>
    {
        public ServiceResponse(T data) {
          Data = data;
        }

        public T Data { get; set; }
    }
}