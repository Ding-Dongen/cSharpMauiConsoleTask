

using Business.Interfaces;
using UserManagementApp;

namespace MauiTask
{
    public partial class App : Application
    {
        private readonly IUserService _userService;

        public App(IUserService userService)
        {
            InitializeComponent();
            _userService = userService;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
                if (DeviceInfo.Current.Platform.ToString() == "WinUI")
            {
                return new Window(new AppShell(_userService))
                {
                    Width = 500,
                    Height = 1000,
                    X = 10,
                    Y = 10
                };
            }
                return new Window(new AppShell(_userService));
        }
    }
}