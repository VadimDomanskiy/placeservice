using Microsoft.AspNetCore.JsonPatch.Exceptions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using PlaceService.Application.ResponseWrapper;

namespace PlaceService.Api.Middleware
{
    public sealed class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IActionResultExecutor<ObjectResult> _actionResultExecutor;

        public ErrorHandlerMiddleware(RequestDelegate next,
            IActionResultExecutor<ObjectResult> actionResultExecutor)
        {
            _next = next;
            _actionResultExecutor = actionResultExecutor;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (InvalidDataException ex)
            {
                await SendResponseAsync(context, new BadRequestObjectResult(Response<InvalidDataException>.CreateError(ex.Message)));
            }
            catch (JsonPatchException ex)
            {
                await SendResponseAsync(context, new BadRequestObjectResult(Response<JsonPatchException>.CreateError(ex.Message)));
            }
            catch (Exception ex)
            {
                await SendResponseAsync(context, new BadRequestObjectResult(Response<string>.CreateError(ex.Message)));
            }
        }

        private Task SendResponseAsync(HttpContext context, ObjectResult objectResult) =>
                _actionResultExecutor.ExecuteAsync(new ActionContext() { HttpContext = context }, objectResult);
    }
}
