using Business.Interfaces;
using Business.Models;

namespace cSharpConsoleApp
{
    public class UserConsoleInterface
    {
        private readonly IUserService _userService;

        public UserConsoleInterface(IUserService userService)
        {
            _userService = userService;
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("\nUser Management App");
                Console.WriteLine("1. Add User");
                Console.WriteLine("2. Show Users");
                Console.WriteLine("3. Update User");
                Console.WriteLine("4. Delete User");
                Console.WriteLine("5. Find User By Email");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid option. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        AddUser();
                        break;
                    case 2:
                        ShowUsers();
                        break;
                    case 3:
                        UpdateUser();
                        break;
                    case 4:
                        DeleteUser();
                        break;
                    case 5:
                        FindUserByEmail();
                        break;
                    case 6:
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        private void AddUser()
        {
            Console.Write("Enter user name: ");
            string name = Console.ReadLine()!;
            Console.Write("Enter user last name: ");
            string lastname = Console.ReadLine()!;
            Console.Write("Enter email: ");
            string email = Console.ReadLine()!;
            Console.Write("Enter password: ");
            string password = Console.ReadLine()!;
            Console.Write("Enter phone number: ");
            int phoneNumber = int.Parse(Console.ReadLine()!);
            Console.Write("Enter address: ");
            string address = Console.ReadLine()!;
            Console.Write("Enter zip code: ");
            int zipCode = int.Parse(Console.ReadLine()!);
            Console.Write("Enter city: ");
            string city = Console.ReadLine()!;


            try
            {
                var user = new User
                {
                    Name = name,
                    LastName = lastname,
                    Email = email,
                    Password = password,
                    PhoneNumber = phoneNumber,
                    Address = address,
                    ZipCode = zipCode,
                    City = city
                };

                _userService.AddUserAsync(user).Wait();
                Console.WriteLine("User added successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        private void ShowUsers()
        {
            var users = _userService.GetAllUsersAsync().Result;
            if (users.Count == 0)
            {
                Console.WriteLine("No users found.");
                return;
            }
            foreach (var user in users)
            {
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine($"User found with ID: {user.Id}");
                Console.WriteLine($"Name: {user.Name}");
                Console.WriteLine($"Last Name: {user.LastName}");
                Console.WriteLine($"Email: {user.Email}");
                Console.WriteLine($"Phone Number: {user.PhoneNumber}");
                Console.WriteLine($"Address: {user.Address}");
                Console.WriteLine($"Zip Code: {user.ZipCode}");
                Console.WriteLine($"City: {user.City}");
            }
        }

        private void UpdateUser()
        {
            Console.Write("Enter user email to update: ");
            string email = Console.ReadLine()!;

            var user = _userService.GetAllUsersAsync().Result.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                Console.Write("Enter new name: ");
                string newName = Console.ReadLine()!;
                Console.Write("Enter new last name: ");
                string newLastName = Console.ReadLine()!;
                Console.Write("Enter new email: ");
                string newEmail = Console.ReadLine()!;
                Console.Write("Enter new password: ");
                string newPassword = Console.ReadLine()!;
                Console.Write("Enter new phone number: ");
                int newPhoneNumber = int.Parse(Console.ReadLine()!);
                Console.Write("Enter new address: ");
                string newAddress = Console.ReadLine()!;
                Console.Write("Enter new zip code: ");
                int newZipCode = int.Parse(Console.ReadLine()!);
                Console.Write("Enter new city: ");
                string newCity = Console.ReadLine()!;

                user.Name = string.IsNullOrWhiteSpace(newName) ? user.Name : newName;
                user.LastName = string.IsNullOrWhiteSpace(newLastName) ? user.LastName : newLastName;
                user.Email = string.IsNullOrWhiteSpace(newEmail) ? user.Email : newEmail;
                user.Password = string.IsNullOrWhiteSpace(newPassword) ? user.Password : newPassword;
                user.PhoneNumber = newPhoneNumber == 0 ? user.PhoneNumber : newPhoneNumber;
                user.Address = string.IsNullOrWhiteSpace(newAddress) ? user.Address : newAddress;
                user.ZipCode = newZipCode == 0 ? user.ZipCode : newZipCode;
                user.City = string.IsNullOrWhiteSpace(newCity) ? user.City : newCity;

                _userService.EditUserAsync(user).Wait();
                Console.WriteLine("User updated successfully.");
            }
            else
            {
                Console.WriteLine("User not found.");
            }
        }

        private void DeleteUser()
        {
            Console.Write("Enter user email to delete: ");
            string email = Console.ReadLine()!;

            var user = _userService.GetAllUsersAsync().Result.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                try
                {
                    _userService.DeleteUserAsync(user.Id.ToString()).Wait();
                    Console.WriteLine("User deleted successfully.");
                }
                catch (Exception)
                {
                    Console.WriteLine("User not found.");
                }
            }
            else
            {
                Console.WriteLine("No user found with that email.");
            }
        }

        private void FindUserByEmail()
        {
            Console.Write("Enter email to search: ");
            string email = Console.ReadLine()!;
            var user = _userService.GetAllUsersAsync().Result.FirstOrDefault(u => u.Email == email);

            if (user != null)
            {
                Console.WriteLine($"User found with ID: {user.Id}");
                Console.WriteLine($"Name: {user.Name}");
                Console.WriteLine($"Last Name: {user.LastName}");
                Console.WriteLine($"Email: {user.Email}");
                Console.WriteLine($"Phone Number: {user.PhoneNumber}");
                Console.WriteLine($"Address: {user.Address}");
                Console.WriteLine($"Zip Code: {user.ZipCode}");
                Console.WriteLine($"City: {user.City}");

            }
            else
            {
                Console.WriteLine("No user found with that email.");
            }
        }
    }
}
