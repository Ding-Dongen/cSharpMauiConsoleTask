using Business.Interfaces;
using Business.Data;

namespace Business.Factories
{
    public class DataStorageFactory : IDataStorageFactory
    {
        private readonly string _storageType;

        public DataStorageFactory(string storageType)
        {
            _storageType = storageType;
        }

        public IDataStorage CreateDataStorage()
        {
            return _storageType switch
            {
                "Json" => new JsonStorage(),
                "Mock" => new MockDataStorage(),
                _ => throw new InvalidOperationException("Invalid storage type specified")
            };
        }
    }
}
