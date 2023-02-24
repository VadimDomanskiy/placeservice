
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace PlaceService.Application.ResponseWrapper
{
    public class Response<T>
    {
        public T Result { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }

        public Response()
        {
            IsSuccess = true;
        }

        public static Response<T> Create(T result)
        {
            return new Response<T> { Result = result };
        }

        public static Response<T> CreateError(string errorMessage)
        {
            return new Response<T>
            {
                IsSuccess = false,
                ErrorMessage = errorMessage
            };
        }

        public static void Write(HttpContext context, Response<T> response)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = response.IsSuccess ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest;
            var json = JsonConvert.SerializeObject(response);
            context.Response.WriteAsync(json);
        }
    }
}
