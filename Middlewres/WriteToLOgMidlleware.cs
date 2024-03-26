using System.Diagnostics;

namespace tasks.Middlewres;

public class WriteToLOgMiddlleware
{
    private readonly RequestDelegate next;
    private readonly ILogger logger;


    public WriteToLOgMiddlleware(RequestDelegate next, ILogger<WriteToLOgMiddlleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task Invoke(HttpContext c)
    {
        var sw = new Stopwatch();
        sw.Start();
        await next.Invoke(c);
        logger.LogDebug($"{c.Request.Path}.{c.Request.Method} took {sw.ElapsedMilliseconds}ms."
            + $" User: {c.User?.FindFirst("userId")?.Value ?? "unknown"}");
        
    }

    

}