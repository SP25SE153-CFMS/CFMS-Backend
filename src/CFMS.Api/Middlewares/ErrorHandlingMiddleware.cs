using CFMS.Application.Events;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly EventQueue _eventQueue;

    public ErrorHandlingMiddleware(RequestDelegate next, EventQueue eventQueue)
    {
        _next = next;
        _eventQueue = eventQueue;
    }

    public async Task Invoke(HttpContext context)
    {
        await _next(context);

        if (_eventQueue.HasErrors())
        {
            var error = _eventQueue.PopError();
            context.Response.StatusCode = 400;
            await context.Response.WriteAsJsonAsync(new { error });
        }
    }
}
