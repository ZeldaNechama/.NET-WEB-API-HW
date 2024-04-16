using Microsoft.Extensions.DependencyInjection;
using tasks.Middlewres;
using tasks.Interfaces;
namespace tasks.Utilities;

public static partial class Utilities
{
    public static void AddTask(this IServiceCollection services)
    {
        services.AddSingleton<Interfaces.IMyTasksServices, Services.MyTaskServicesToFile>();
    }
   

     public static IApplicationBuilder UseMyLogMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<WriteToLOgMiddlleware>();
    }
}

