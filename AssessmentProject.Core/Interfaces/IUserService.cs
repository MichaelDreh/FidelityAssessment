using AssessmentProject.Core.DTOs.User.Request;
using AssessmentProject.Core.DTOs.User.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssessmentProject.Core.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Create a new user for an existing account
        /// </summary>
        /// <param name="accountId">Unique ID of existing account</param>
        /// <param name="user">User DTO</param>
        /// <returns><see cref="CustomResponse"/></returns>
        Task<CustomResponse> CreateUserAsync(int accountId, UserRequest user);

        /// <summary>
        /// Gell all users associated to an account
        /// </summary>
        /// <param name="accountId">Unique ID of account</param>
        /// <returns>List of users</returns>
        Task<List<UserResponse>> GetAccountUsersAsync(int accountId);

        /// <summary>
        /// Get a user associated with an account
        /// </summary>
        /// <param name="accountId">Unique ID of account</param>
        /// <param name="userId">Unique ID of user</param>
        /// <returns>A user</returns>
        Task<UserResponse> GetUserByIdAysnc(int accountId, int userId);

        /// <summary>
        /// Delete a user from an account
        /// </summary>
        /// <param name="accountId">Unique ID of account</param>
        /// <param name="userId">Unique ID of user</param>
        /// <returns><see cref="CustomResponse"/></returns>
        Task<CustomResponse> DeleteUserAync(int accountId, int userId);

        /// <summary>
        /// Update a user of a specific account
        /// </summary>
        /// <param name="accountId">Unique ID of account</param>
        /// <param name="userId">Unique ID of user</param>
        /// <param name="user">User Update DTO</param>
        /// <returns><see cref="CustomResponse"/></returns>
        Task<CustomResponse> UpdateUserAsync(int accountId, int userId, UserRequest user);
    }
}
