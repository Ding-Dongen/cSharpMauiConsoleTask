
using Business.Interfaces;
using Business.Models;

namespace Business.Services
{
    public class UserManager : IUserService
    {
        private readonly IDataStorage _dataStorage;

        public UserManager(IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }

        public async Task AddUserAsync(User user)
        {
            await Task.Run(() => _dataStorage.Save(user));
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await Task.Run(() => _dataStorage.GetAll());
        }

        public async Task DeleteUserAsync(string id)
        {
            await Task.Run(() => _dataStorage.Delete(id));
        }

        public async Task DeleteUserAsync(User? user)
        {
            if (user != null)
            {
                await Task.Run(() => _dataStorage.Delete(user.Id));
            }
        }

        public async Task EditUserAsync(User user)
        {
            await Task.Run(() =>
            {
                var users = _dataStorage.GetAll();
                var existingUser = users.FirstOrDefault(u => u.Id == user.Id);

                if (existingUser != null)
                {
                    existingUser.Name = user.Name;
                    existingUser.LastName = user.LastName;
                    existingUser.Email = user.Email;
                    existingUser.Password = user.Password;
                    existingUser.PhoneNumber = user.PhoneNumber;
                    existingUser.Address = user.Address;
                    existingUser.ZipCode = user.ZipCode;
                    existingUser.City = user.City;


                    _dataStorage.Save(existingUser);
                }
                else
                {
                    throw new KeyNotFoundException($"User with ID {user.Id} not found.");
                }
            });
        }

        public async Task<User?> GetUserByIdAsync(string id)
        {
            return await Task.Run(() => _dataStorage.GetById(id));
        }
    }
}
