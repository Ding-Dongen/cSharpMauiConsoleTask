using Business.Interfaces;
using Business.Models;
using System.Collections.ObjectModel;

namespace UserManagementApp
{
    public partial class ListUsersPage : ContentPage
    {
        private readonly IUserService _userService;

        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();

        public ListUsersPage(IUserService userService)
        {
            InitializeComponent();
            _userService = userService;

            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadUsers();
        }

        private async Task LoadUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            Users.Clear();
            foreach (var user in users)
            {
                Users.Add(user);
            }
        }

        private async void OnUserSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is User selectedUser)
            {
                // Navigate to UserDetailPage with the selected user
                await Navigation.PushAsync(new UserDetailPage(_userService, selectedUser));

                // Deselect the item after navigation
                userListView.SelectedItem = null;
            }
        }
    }
}
