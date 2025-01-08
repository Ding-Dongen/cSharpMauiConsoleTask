
using Moq;
using Business.Models;
using Business.Interfaces;
using Business.Services;
using Business.Data;

namespace BusinessTests.Services
{
    public class UserManagerTests
    {
        private readonly Mock<IDataStorage> _mockDataStorage;
        private readonly UserManager _userManager;

        public UserManagerTests()
        {
            _mockDataStorage = new Mock<IDataStorage>();
            _userManager = new UserManager(_mockDataStorage.Object);
        }

        [Fact]
        public async Task AddUserAsync_ShouldCallSaveOnStorage()
        {
            // Arrange
            var user = new User { Id = Guid.NewGuid(), Name = "Test", LastName = "User" };
            _mockDataStorage.Setup(ds => ds.Save(It.IsAny<User>()));

            // Act
            await _userManager.AddUserAsync(user);

            // Assert
            _mockDataStorage.Verify(ds => ds.Save(user), Times.Once);
        }

        [Fact]
        public async Task GetAllUsersAsync_ShouldReturnAllUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = Guid.NewGuid(), Name = "Test1", LastName = "User1" },
                new User { Id = Guid.NewGuid(), Name = "Test2", LastName = "User2" }
            };
            _mockDataStorage.Setup(ds => ds.GetAll()).Returns(users);

            // Act
            var result = await _userManager.GetAllUsersAsync();

            // Assert
            Assert.Equal(users.Count, result.Count);
            Assert.Equal(users[0].Id, result[0].Id);
            Assert.Equal(users[1].Id, result[1].Id);
        }

        [Fact]
        public async Task DeleteUserAsync_WithValidId_ShouldCallDeleteOnStorage()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            _mockDataStorage.Setup(ds => ds.Delete(It.Is<string>(id => id == userId)));

            // Act
            await _userManager.DeleteUserAsync(userId);

            // Assert
            _mockDataStorage.Verify(ds => ds.Delete(userId), Times.Once);
        }

        [Fact]
        public async Task EditUserAsync_ShouldUpdateExistingUser()
        {
            // Arrange
            var user = new User { Id = Guid.NewGuid(), Name = "Old", LastName = "Name" };
            var updatedUser = new User { Id = user.Id, Name = "New", LastName = "Name" };
            var users = new List<User> { user };
            _mockDataStorage.Setup(ds => ds.GetAll()).Returns(users);
            _mockDataStorage.Setup(ds => ds.Save(It.IsAny<User>()));

            // Act
            await _userManager.EditUserAsync(updatedUser);

            // Assert
            _mockDataStorage.Verify(ds => ds.Save(It.Is<User>(u =>
                u.Id == updatedUser.Id &&
                u.Name == updatedUser.Name &&
                u.LastName == updatedUser.LastName)), Times.Once);
        }

        [Fact]
        public async Task GetUserByIdAsync_WithValidId_ReturnsUser()
        {
            // Arrange
            var user = new User { Id = Guid.NewGuid(), Name = "Test", LastName = "User" };
            _mockDataStorage.Setup(ds => ds.GetById(It.Is<string>(id => id == user.Id.ToString()))).Returns(user);

            // Act
            var result = await _userManager.GetUserByIdAsync(user.Id.ToString());

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.Name, result.Name);
            Assert.Equal(user.LastName, result.LastName);
        }
        [Fact]
        public async Task EditUserAsync_WithNonexistentId_ThrowsKeyNotFoundException()
        {
            // Arrange
            var mockDataStorage = new Mock<IDataStorage>();
            var userManager = new UserManager(mockDataStorage.Object);

            // Simulate that no users exist in the storage
            mockDataStorage.Setup(ds => ds.GetAll()).Returns(new List<User>());

            var user = new User { Id = Guid.NewGuid(), Name = "Nonexistent User" };

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(async () => await userManager.EditUserAsync(user));
        }


        [Fact]
        public void Save_NewUser_AddsUserToStorage()
        {
            // Arrange
            var storage = new MockDataStorage();
            var user = new User { Id = Guid.NewGuid(), Name = "Test User", Email = "test@example.com" };

            // Act
            storage.Save(user);
            var result = storage.GetAll();

            // Assert
            Assert.Single(result);
            Assert.Equal(user.Id, result.First().Id);
            Assert.Equal(user.Name, result.First().Name);
        }


    }
}