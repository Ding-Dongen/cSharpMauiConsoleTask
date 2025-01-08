using System.Windows.Input;
using Business.Interfaces;

namespace UserManagementApp
{
    public partial class AppShell : Shell
    {
        private readonly IUserService _userService;

        public AppShell(IUserService userService)
        {
            InitializeComponent();
            _userService = userService;
            BindingContext = this;
            BackgroundColor = Color.FromArgb("#4B0082");


            Routing.RegisterRoute(nameof(ListUsersPage), typeof(ListUsersPage));
            Routing.RegisterRoute(nameof(AddUserPage), typeof(AddUserPage));
            Routing.RegisterRoute(nameof(EditUserPage), typeof(EditUserPage));
            //Routing.RegisterRoute(nameof(ShowUsersPage), typeof(ShowUsersPage));

           
            AddUserCommand = new Command(ExecuteAddUserCommand);
        }

        public ICommand AddUserCommand { get; }

        private async void ExecuteAddUserCommand()
        {
            await Shell.Current.GoToAsync(nameof(AddUserPage));
        }

        private async void OnListUsersClicked(object sender, System.EventArgs e)
        {
            
            await Shell.Current.GoToAsync(nameof(ListUsersPage));
        }

        //private async void OnShowUsersClicked(object sender, System.EventArgs e)
        //{
            
        //    await Shell.Current.GoToAsync(nameof(ShowUsersPage));


        //}
    }
}