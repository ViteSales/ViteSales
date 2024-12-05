using ViteSales.API.Models;

namespace ViteSales.API.Middleware;

public class ExceptionHandlerMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext ctx)
    {
        try
        {
            await next(ctx);
        }
        catch (Exception e)
        {
            var response = new ErrorResponse()
            {
                Message = $"{e.Message} \n \n ${e.InnerException?.Message}",
                Stack = $"{e.StackTrace} \n \n ${e.InnerException?.StackTrace}",
                Data = e.Data,
                Status = false
            };
            ctx.Response.StatusCode = 502;
            await ctx.Response.Body.WriteAsync(response.ToBytes());
        }
    }
}