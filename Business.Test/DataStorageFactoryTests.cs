using Business.Data;
using Business.Factories;
using Microsoft.Extensions.Configuration;

namespace Business.Tests
{
    public class DataStorageFactoryTests
    {
        [Fact]
        public void CreateDataStorage_ReturnsJsonStorage_WhenConfiguredAsJson()
        {
            // Arrange
            var inMemorySettings = new Dictionary<string, string>
            {
                { "DataStorageType", "Json" }
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings as IEnumerable<KeyValuePair<string, string?>>)
                .Build();

            var storageType = configuration["DataStorageType"];
            Assert.NotNull(storageType); 
            var factory = new DataStorageFactory(storageType!);


            // Act
            var dataStorage = factory.CreateDataStorage();

            // Assert
            Assert.IsType<JsonStorage>(dataStorage);
        }

        [Fact]
        public void CreateDataStorage_ThrowsException_WhenInvalidTypeConfigured()
        {
            // Arrange
            var inMemorySettings = new Dictionary<string, string>
            {
                { "DataStorageType", "InvalidType" }
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings as IEnumerable<KeyValuePair<string, string?>>)
                .Build();

            var storageType = configuration["DataStorageType"];
            Assert.NotNull(storageType); // Ensure storageType is not null
            var factory = new DataStorageFactory(storageType!);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => factory.CreateDataStorage());
        }

        [Fact]
        public void CreateDataStorage_WithNullStorageType_ThrowsException()
        {
            var factory = new DataStorageFactory(null!);
            Assert.Throws<InvalidOperationException>(() => factory.CreateDataStorage());
        }

    }
}
