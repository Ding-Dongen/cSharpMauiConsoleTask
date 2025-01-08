using Business.Models;

namespace Business.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Adds a new user to the data storage asynchronously.
        /// </summary>
        /// <param name="user">The user to be added.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        Task AddUserAsync(User user);

        /// <summary>
        /// Retrieves all users from the data storage asynchronously.
        /// </summary>
        /// <returns>A Task that returns a list of users.</returns>
        Task<List<User>> GetAllUsersAsync();

        /// <summary>
        /// Deletes a user by their ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        Task DeleteUserAsync(string id);

        /// <summary>
        /// Updates an existing user's information asynchronously.
        /// </summary>
        /// <param name="user">The updated user information.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        Task EditUserAsync(User user);

        /// <summary>
        /// Retrieves a user by their ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>A Task that returns the user object, or null if not found.</returns>
        Task<User?> GetUserByIdAsync(string id);
        Task DeleteUserAsync(User? user);
    }
}
