using Microsoft.Extensions.DependencyInjection;
using tasks.Middlewres;

namespace tasks.Utilities;

public static class Utilities
{
    public static void AddTask(this IServiceCollection services)
    {
        services.AddSingleton<Interfaces.IMyTasksServices, Services.MyTaskService>();
    }

     public static IApplicationBuilder UseMyLogMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<WriteToLOgMiddlleware>();
    }
}

