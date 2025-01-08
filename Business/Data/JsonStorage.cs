
using System.Diagnostics;
using System.Text.Json;
using Business.Interfaces;
using Business.Models;

namespace Business.Data
{
    public class JsonStorage : IDataStorage
    {
        private const string FileName = "users.json";
        private readonly string _filePath;

        public JsonStorage()
        {
            _filePath = GetFilePath();
        }

        private string GetFilePath()
        {
            // Here you define a common place for the file. This example uses AppData for simplicity,
            // but you might want a shared location or cloud storage for true cross-platform sharing.
#if ANDROID
            return Path.Combine(FileSystem.AppDataDirectory, FileName);
#else
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), FileName);
#endif
        }

        public void Save(User user)
        {
            var users = GetAll();

            
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
            }
            else
            {
              
                users.Add(user);
            }

            
            var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        public List<User> GetAll()
        {
            if (!File.Exists(_filePath))
            {
                Debug.WriteLine($"File not found: {_filePath}");
                return new List<User>();
            }

            var json = File.ReadAllText(_filePath);
            Debug.WriteLine($"File content: {json}");
            return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
        }

        public User? GetById(string id)
        {
            if (Guid.TryParse(id, out var guid))
            {
                var users = GetAll();
                return users.FirstOrDefault(u => u.Id == guid);
            }
            else
            {
                throw new ArgumentException("Invalid GUID format", nameof(id));
            }
        }

        public void Delete(string id)
        {
            if (Guid.TryParse(id, out var guid))
            {
                Delete(guid);
            }
            else
            {
                throw new ArgumentException("Invalid GUID format", nameof(id));
            }
        }

        public void Delete(Guid id)
        {
            var users = GetAll();
            var userToRemove = users.FirstOrDefault(u => u.Id == id);
            if (userToRemove != null)
            {
                users.Remove(userToRemove);

                
                var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_filePath, json);
            }
        }
    }
}
