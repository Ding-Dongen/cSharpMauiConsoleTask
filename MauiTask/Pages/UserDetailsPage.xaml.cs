using Business.Interfaces;
using Business.Models;

namespace UserManagementApp
{
    public partial class UserDetailPage : ContentPage
    {
        private readonly IUserService _userService;
        private readonly User _user;

        public UserDetailPage(IUserService userService, User user)
        {
            InitializeComponent();
            _userService = userService;
            _user = user;


            BindingContext = new
            {
                User = _user, 
                CancelCommand = new Command(async () => await Cancel())
            };
        }

        private async Task Cancel()
        {
            await Navigation.PopAsync();
        }

        private async void OnDeleteUserClicked(object sender, EventArgs e)
        {
            var confirm = await DisplayAlert("Confirm Delete", $"Are you sure you want to delete {_user.Name}?", "Yes", "No");
            if (confirm)
            {
                await _userService.DeleteUserAsync(_user.Id.ToString());
                await DisplayAlert("Deleted", $"{_user.Name} has been deleted.", "OK");
                await Navigation.PopAsync(); 
            }
        }

        private async void OnEditUserClicked(object sender, EventArgs e)
        {
            var editPage = new EditUserPage(_userService, _user);
            await Navigation.PushAsync(editPage);
        }

        public Command CancelCommand => new Command(async () =>
        {
            await Navigation.PopAsync(); 
        });
    }
}
