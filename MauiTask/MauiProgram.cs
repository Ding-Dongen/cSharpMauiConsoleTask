using Business.Factories;
using UserManagementApp;
using Business.Interfaces;
using Business.Services;

namespace MauiTask
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            
            builder.Services.AddSingleton<IDataStorageFactory>(provider =>
                new DataStorageFactory("Json")); 
            builder.Services.AddSingleton<IDataStorage>(provider =>
                provider.GetRequiredService<IDataStorageFactory>().CreateDataStorage());
            builder.Services.AddSingleton<IUserService, UserManager>();
            builder.Services.AddSingleton<AppShell>();

            
            builder.Services.AddTransient<AddUserPage>();
            builder.Services.AddTransient<EditUserPage>();
            builder.Services.AddTransient<ListUsersPage>();
            //builder.Services.AddTransient<ShowUsersPage>();

            return builder.Build();
        }
    }
}
