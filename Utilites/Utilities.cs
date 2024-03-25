using Microsoft.Extensions.DependencyInjection;

namespace tasks.Utilities;

public static class Utilities
{
    public static void AddTask(this IServiceCollection services)
        {
            services.AddSingleton<Interfaces.IMyTasksServices,Services.MyTaskService>();
        }
}

