using System.Diagnostics;

namespace tasks.Middlewres;

public class WriteToLOgMiddlleware
{
    private readonly RequestDelegate next;
    private readonly string path;


    public WriteToLOgMiddlleware(RequestDelegate next, string path)
    {
        this.next = next;
        this.path = path;
    }

    public async Task Invoke(HttpContext c)
    {
        var sw = new Stopwatch();
        sw.Start();
        await next(c);
        WriteLogToFile($"{c.Request.Path}.{c.Request.Method} took {sw.ElapsedMilliseconds}ms."
            + $" User: {c.User?.FindFirst("name")?.Value ?? "unknown"}");

    }
    private void WriteLogToFile(string logMessage)
    {
        const int maxAttempts = 3;
        const int delayMs = 100;

        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            try
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(logMessage);
                }
                return;
            }
            catch (IOException)
            {
                if (attempt < maxAttempts)
                    Task.Delay(delayMs).Wait();
                else
                    Console.WriteLine($"Failed to write to log file after {maxAttempts} attempts.");

            }
        }


    }
}
    public static partial class Utilities
    {
        public static IApplicationBuilder LogMiddleware(this IApplicationBuilder applicationBuilder, string path)
        {
            return applicationBuilder.UseMiddleware<WriteToLOgMiddlleware>(path);
        }
    }
