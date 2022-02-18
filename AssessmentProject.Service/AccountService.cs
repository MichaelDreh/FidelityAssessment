using AssessmentProject.Core;
using AssessmentProject.Core.DTOs.Account.Request;
using AssessmentProject.Core.DTOs.Account.Response;
using AssessmentProject.Core.DTOs.User.Response;
using AssessmentProject.Core.Entities;
using AssessmentProject.Core.Interfaces;
using AssessmentProject.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssessmentProject.Service
{
    public class AccountService : IAccountService
    {
        private readonly AssessmentContext _dbContext;

        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public AccountService(AssessmentContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CustomResponse> CreateAccountAsync(AccountRequest account)
        {
            CustomResponse result = new CustomResponse();
            try
            {
                var acct = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.CompanyName == account.CompanyName);
                if (acct != null)
                {
                    return new CustomResponse
                    {
                        ResponseCode = "01",
                        ResponseMessage = "Account With The CompanyName Already Exist In Our Record"
                    };
                }
                else
                {
                    _dbContext.Accounts.Add(new Account
                    {
                        CompanyName = account.CompanyName,
                        Website = account.Website,
                        DateCreated = DateTime.Now
                    });

                    await _dbContext.SaveChangesAsync();

                    result = new CustomResponse
                    {
                        ResponseCode = "00",
                        ResponseMessage = "New Account Created"
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                result = new CustomResponse
                {
                    ResponseCode = "02",
                    ResponseMessage = "Error Creating New Record"
                };
            }
            return result;
        }

        public async Task<CustomResponse> DeleteAccountAsync(int accountId)
        {
            CustomResponse result = new CustomResponse();
            try
            {
                var acct = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);
                if (acct != null)
                {
                    _dbContext.Accounts.Remove(acct);
                    await _dbContext.SaveChangesAsync();

                    result = new CustomResponse
                    {
                        ResponseCode = "00",
                        ResponseMessage = "Account Deleted"
                    };
                }
                else
                {
                    return new CustomResponse
                    {
                        ResponseCode = "01",
                        ResponseMessage = "Account Id Not Found"
                    };
                }
            }
            catch (Exception ex)
            {
                result = new CustomResponse
                {
                    ResponseCode = "02",
                    ResponseMessage = "Error Deleting The Account"
                };
                _logger.Error(ex);
            }

            return result;
        }

        public async Task<AccountResponse> GetAccountByIdAsync(int accountId)
        {
            AccountResponse result = new AccountResponse();
            try
            {
                var account = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);

                if (account != null)
                {
                    var users = _dbContext.Users.Where(a => a.AccountId == accountId).Select(item=> new UserResponse
                    {
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        Email = item.Email,
                        Id = item.Id
                    }).ToList();

                    result = new AccountResponse
                    {
                        Id = accountId,
                        CompanyName = account.CompanyName,
                        Website = account.Website,
                        Users = users
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }

            return result;
        }

        public async Task<List<AccountResponse>> GetAllAccountsAsync()
        {
            List<AccountResponse> result = new List<AccountResponse>();
            try
            {
                var accounts = await _dbContext.Accounts.AsNoTracking().ToListAsync();
                if(accounts != null && accounts.Any())
                {
                    result = accounts.Select(item => new AccountResponse
                    {
                        Id = item.Id,
                        CompanyName = item.CompanyName,
                        Website = item.Website
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }

            return result;
        }

        public async Task<CustomResponse> UpdateAccountAsync(int accountId, AccountRequest account)
        {
            CustomResponse result = new CustomResponse();
            try
            {
                var acct = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);
                if (acct == null)
                {
                    return new CustomResponse
                    {
                        ResponseCode = "01",
                        ResponseMessage = "Account With The Id Not Found"
                    };
                }
                else
                {
                    acct.CompanyName = string.IsNullOrEmpty(account.CompanyName) ? acct.CompanyName : account.CompanyName;
                    acct.Website = string.IsNullOrEmpty(account.Website) ? acct.Website : account.Website;

                    _dbContext.Entry(acct).State = EntityState.Modified;
                    await _dbContext.SaveChangesAsync();

                    result = new CustomResponse
                    {
                        ResponseCode = "00",
                        ResponseMessage = "Account Updated"
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                result = new CustomResponse
                {
                    ResponseCode = "02",
                    ResponseMessage = "An error occured while updating record"
                };
            }
            return result;
        }
    }
}
