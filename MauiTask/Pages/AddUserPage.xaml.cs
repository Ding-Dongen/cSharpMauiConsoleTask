using Business.Interfaces;

namespace UserManagementApp
{
    public partial class AddUserPage : ContentPage
    {
        private readonly IUserService _userService;

        public AddUserPage(IUserService userService)
        {
            InitializeComponent();
            _userService = userService;
        }

        private async void OnAddUserClicked(object sender, EventArgs e)
        {

            var user = new Business.Models.User
            {
                Name = nameEntry.Text,
                LastName = lastNameEntry.Text,
                Email = emailEntry.Text,
                Password = passwordEntry.Text,
                PhoneNumber = int.Parse(phoneNumberEntry.Text),
                Address = addressEntry.Text,
                ZipCode = int.Parse(zipCodeEntry.Text),
                City = cityEntry.Text
            };

            await _userService.AddUserAsync(user);
            await DisplayAlert("Success", "User added successfully", "OK");
            await Navigation.PopAsync();


            nameEntry.Text = string.Empty;
            lastNameEntry.Text = string.Empty;
            emailEntry.Text = string.Empty;
            passwordEntry.Text = string.Empty;
            phoneNumberEntry.Text = string.Empty;
            addressEntry.Text = string.Empty;
            zipCodeEntry.Text = string.Empty;
            cityEntry.Text = string.Empty;
        }
    }
}