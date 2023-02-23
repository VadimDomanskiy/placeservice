
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
    /*internal class ResponseWrapper<T>
    {
        public ResponseWrapper()
        {
            Errors = new List<string>();
        }

        public ResponseWrapper(T data) : this()
        {
            Data = data;
        }

        public ResponseWrapper(string error) : this()
        {
            Errors.Add(error);
        }

        public ResponseWrapper(IEnumerable<string> errors) : this()
        {
            Errors.AddRange(errors);
        }

        public T Data { get; init; }
        public bool Success => !Errors.Any();
        public List<string> Errors { get; set; }

        public HttpStatusCode? StatusCode { get; init; }
    }*/
}
