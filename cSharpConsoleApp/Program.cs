using Microsoft.Extensions.DependencyInjection;
using Business.Data;
using Business.Interfaces;
using Business.Services;
using cSharpConsoleApp;

public static class Program
{
    public static void Main()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        var serviceProvider = services.BuildServiceProvider();
        var userConsoleInterface = serviceProvider.GetRequiredService<UserConsoleInterface>();
        userConsoleInterface.Run();
    }
    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IDataStorage, JsonStorage>();
        services.AddScoped<IUserService, UserManager>();
        services.AddScoped<UserConsoleInterface>();
    }
}

