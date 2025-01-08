
using Business.Interfaces;
using Business.Models;

namespace UserManagementApp
{
    public partial class EditUserPage : ContentPage
    {
        private readonly IUserService _userService;
        private User _user;

        public EditUserPage(IUserService userService, User user)
        {
            InitializeComponent();
            _userService = userService;
            _user = user;
            nameEntry.Text = user.Name;
            lastNameEntry.Text = user.LastName;
            emailEntry.Text = user.Email;
            passwordEntry.Text = user.Password;
            phoneNumberEntry.Text = user.PhoneNumber.ToString();
            addressEntry.Text = user.Address;
            zipCodeEntry.Text = user.ZipCode.ToString();
            cityEntry.Text = user.City;

        }

        private async void OnUpdateUserClicked(object sender, EventArgs e)
        {
            _user.Name = nameEntry.Text;
            _user.LastName = lastNameEntry.Text;
            _user.Email = emailEntry.Text;
            _user.Password = passwordEntry.Text;
            _user.PhoneNumber = int.Parse(phoneNumberEntry.Text);
            _user.Address = addressEntry.Text;
            _user.ZipCode = int.Parse(zipCodeEntry.Text);
            _user.City = cityEntry.Text;

            await _userService.EditUserAsync(_user);
            await DisplayAlert("Success", "User updated successfully", "OK");
            await Navigation.PopAsync();
        }
    }
}