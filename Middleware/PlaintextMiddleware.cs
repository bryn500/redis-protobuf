using System.Text;

namespace Redis.MiddleWare;

public class PlaintextMiddleware
{
    private static readonly PathString _path = new PathString("/");
    private static readonly byte[] _helloWorldPayload = Encoding.UTF8.GetBytes($"/swagger/index.html{Environment.NewLine}/api");

    private readonly RequestDelegate _next;

    public PlaintextMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public Task Invoke(HttpContext httpContext)
    {
        if (httpContext.Request.Path.StartsWithSegments(_path, StringComparison.Ordinal))
        {
            return WriteResponse(httpContext.Response);
        }

        return _next(httpContext);
    }

    public static Task WriteResponse(HttpResponse response)
    {
        var payloadLength = _helloWorldPayload.Length;
        response.StatusCode = 200;
        response.ContentType = "text/plain";
        response.ContentLength = payloadLength;
        return response.Body.WriteAsync(_helloWorldPayload, 0, payloadLength);
    }
}

public static class PlaintextMiddlewareExtensions
{
    public static IApplicationBuilder UsePlainText(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<PlaintextMiddleware>();
    }
}

