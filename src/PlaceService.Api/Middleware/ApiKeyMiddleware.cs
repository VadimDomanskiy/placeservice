namespace PlaceService.Api.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string ApiKeyName = "ApiKey";
        private readonly string _apiKey;

        public ApiKeyMiddleware(RequestDelegate next, string apiKey)
        {
            _next = next;
            _apiKey = apiKey;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(ApiKeyName, out var apiKeyValue) || apiKeyValue != _apiKey)
            {
                context.Response.StatusCode = 401;
                return;
            }

            await _next(context);
        }
    }

}
