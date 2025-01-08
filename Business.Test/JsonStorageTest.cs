
using Business.Data;
using Business.Models;
using Business.Interfaces;

namespace BusinessTests.Data
{
    public class JsonStorageTests
    {
        private readonly IDataStorage _storage;

        public JsonStorageTests()
        {
            _storage = new MockDataStorage();
        }

        [Fact]
        public void GetAll_EmptyStorage_ReturnsEmptyList()
        {
            // Act
            var result = _storage.GetAll();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void Save_NewUser_AddsUserToStorage()
        {
            // Arrange
            var user = new User { Id = Guid.NewGuid(), Name = "Test", LastName = "User", Email = "test@example.com" };

            // Act
            _storage.Save(user);
            var savedUsers = _storage.GetAll();

            // Assert
            Assert.Single(savedUsers);
            Assert.Equal(user.Id, savedUsers.First().Id);
            Assert.Equal(user.Name, savedUsers.First().Name);
            Assert.Equal(user.LastName, savedUsers.First().LastName);
            Assert.Equal(user.Email, savedUsers.First().Email);
        }

        [Fact]
        public void Save_UpdateExistingUser_UpdatesUserInStorage()
        {
            // Arrange
            var user = new User { Id = Guid.NewGuid(), Name = "Test", LastName = "User", Email = "test@example.com" };
            _storage.Save(user);
            var updatedUser = new User { Id = user.Id, Name = "Updated", LastName = "User", Email = "updated@example.com" };

            // Act
            _storage.Save(updatedUser);
            var savedUsers = _storage.GetAll();

            // Assert
            Assert.Single(savedUsers);
            Assert.Equal(updatedUser.Id, savedUsers.First().Id);
            Assert.Equal(updatedUser.Name, savedUsers.First().Name);
            Assert.Equal(updatedUser.LastName, savedUsers.First().LastName);
            Assert.Equal(updatedUser.Email, savedUsers.First().Email);
        }

        [Fact]
        public void GetById_ValidId_ReturnsUser()
        {
            // Arrange
            var user = new User { Id = Guid.NewGuid(), Name = "Test", LastName = "User" };
            _storage.Save(user);

            // Act
            var result = _storage.GetById(user.Id.ToString());

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.Name, result.Name);
            Assert.Equal(user.LastName, result.LastName);
        }

        [Fact]
        public void GetById_InvalidGuid_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _storage.GetById("not-a-guid"));
        }

        [Fact]
        public void Delete_RemovesUserFromStorage()
        {
            // Arrange
            var user = new User { Id = Guid.NewGuid(), Name = "Test", LastName = "User" };
            _storage.Save(user);

            // Act
            _storage.Delete(user.Id);
            var savedUsers = _storage.GetAll();

            // Assert
            Assert.Empty(savedUsers);
        }


        [Fact]
        public void Delete_ByGuid_RemovesUserFromStorage()
        {
            // Arrange
            var storage = new MockDataStorage(); // Ensure you initialize the storage object
            var user = new User { Id = Guid.NewGuid(), Name = "Test User" };
            storage.Save(user);

            // Act
            storage.Delete(user.Id);

            // Assert
            var result = storage.GetAll();
            Assert.Empty(result);
        }
    }
}