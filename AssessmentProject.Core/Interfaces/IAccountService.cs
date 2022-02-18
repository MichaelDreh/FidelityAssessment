using AssessmentProject.Core.DTOs.Account.Request;
using AssessmentProject.Core.DTOs.Account.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssessmentProject.Core.Interfaces
{
    public interface IAccountService
    {
        /// <summary>
        /// Create a new account
        /// </summary>
        /// <param name="account">Account DTO</param>
        /// <returns><see cref="CustomResponse"/></returns>
        Task<CustomResponse> CreateAccountAsync(AccountRequest account);

        /// <summary>
        /// Get all accounts
        /// </summary>
        /// <returns>List of accounts</returns>
        Task<List<AccountResponse>> GetAllAccountsAsync();

        /// <summary>
        /// Retrieve an account by ID (plus all associated users)
        /// </summary>
        /// <param name="accountId">Unique ID of account to retrieve</param>
        /// <returns>An account</returns>
        Task<AccountResponse> GetAccountByIdAsync(int accountId);

        /// <summary>
        /// Delete a specific account
        /// </summary>
        /// <param name="accountId">Unique ID of account to delete</param>
        /// <returns><see cref="CustomResponse"/></returns>
        Task<CustomResponse> DeleteAccountAsync(int accountId);

        /// <summary>
        /// Update a specific account
        /// </summary>
        /// <param name="accountId">Unique ID of account</param>
        /// <param name="account">Account Update DTO</param>
        /// <returns><see cref="CustomResponse"/></returns>
        Task<CustomResponse> UpdateAccountAsync(int accountId, AccountRequest account);
    }
}
