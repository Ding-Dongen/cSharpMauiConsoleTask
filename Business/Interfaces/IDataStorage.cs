using Business.Models;

namespace Business.Interfaces
{
    public interface IDataStorage
    {
        
        void Save(User user);
        List<User> GetAll();
        User? GetById(string id);
        void Delete(string id);
        void Delete(Guid id);
    }
}
