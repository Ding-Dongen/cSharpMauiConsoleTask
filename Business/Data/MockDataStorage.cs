using Business.Interfaces;
using Business.Models;

namespace Business.Data
{
    public class MockDataStorage : IDataStorage
    {
        private readonly List<User> _users = new();

        public void Save(User user)
        {
            
            var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser != null)
            {
               
                existingUser.Name = user.Name;
                existingUser.LastName = user.LastName;
                existingUser.Email = user.Email;
                existingUser.Password = user.Password;
            }
            else
            {
                
                _users.Add(user);
            }
        }

        public List<User> GetAll()
        {
            return _users;
        }

        public User? GetById(string id) 
        {
            if (Guid.TryParse(id, out var guid))
            {
                return _users.FirstOrDefault(u => u.Id == guid);
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
                var userToRemove = _users.FirstOrDefault(u => u.Id == guid);
                if (userToRemove != null)
                {
                    _users.Remove(userToRemove);
                }
            }
            else
            {
                throw new ArgumentException("Invalid GUID format", nameof(id));
            }
        }

        public void Delete(Guid id) 
        {
            var userToRemove = _users.FirstOrDefault(u => u.Id == id);
            if (userToRemove != null)
            {
                _users.Remove(userToRemove);
            }
        }
    }
}
