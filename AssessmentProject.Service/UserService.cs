using AssessmentProject.Core;
using AssessmentProject.Core.DTOs.User.Request;
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
    public class UserService : IUserService
    {
        private readonly AssessmentContext _dbContext;

        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public UserService(AssessmentContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CustomResponse> CreateUserAsync(int accountId, UserRequest user)
        {
            CustomResponse result = new CustomResponse();
            try
            {
                var account = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);

                if (account == null)
                {
                    return new CustomResponse
                    {
                        ResponseCode = "01",
                        ResponseMessage = "Account Not Found"
                    };
                }
                else
                {
                    var usr = await _dbContext.Users.FirstOrDefaultAsync(a => a.Email.ToLower() == user.Email.ToLower());

                    if (usr != null)
                    {
                        return new CustomResponse
                        {
                            ResponseCode = "01",
                            ResponseMessage = "Email Exists In Our DB"
                        };
                    }
                    else
                    {
                        usr = new User
                        {
                            AccountId = accountId,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Email = user.Email,
                            DateCreated = DateTime.Now
                        };
                        await _dbContext.Users.AddAsync(usr);
                        await _dbContext.SaveChangesAsync();

                        result = new CustomResponse
                        {
                            ResponseCode = "00",
                            ResponseMessage = "User Created"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                result = new CustomResponse
                {
                    ResponseCode = "01",
                    ResponseMessage = "Error Creating New User"
                };
                _logger.Error(ex);
            }

            return result;
        }

        public async Task<CustomResponse> DeleteUserAync(int accountId, int userId)
        {
            CustomResponse result = new CustomResponse();

            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(a => a.AccountId == accountId && a.Id == userId);
                if (user == null)
                {
                    return new CustomResponse
                    {
                        ResponseCode = "01",
                        ResponseMessage = "Error: Account or User Not Found"
                    };
                }
                else
                {
                     _dbContext.Users.Remove(user);
                    await _dbContext.SaveChangesAsync();

                    result = new CustomResponse
                    {
                        ResponseCode = "00",
                        ResponseMessage = "User Deleted"
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                result = new CustomResponse
                {
                    ResponseCode = "02",
                    ResponseMessage = "Error!"
                };

            }

            return result;
        }

        public async Task<List<UserResponse>> GetAccountUsersAsync(int accountId)
        {
            List<UserResponse> result = new List<UserResponse>();
            try
            {
                var users = await _dbContext.Users.Where(a => a.AccountId == accountId).ToListAsync();

                if (users != null && users.Any())
                {
                    result = users.Select(a => new UserResponse
                    {
                        Id = a.Id,
                        FirstName = a.FirstName,
                        LastName = a.LastName,
                        Email = a.Email
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return result;
        }

        public async Task<UserResponse> GetUserByIdAysnc(int accountId, int userId)
        {
            var result = new UserResponse();
            try
            {
                var account = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);
                if (account != null)
                {
                    var user = await _dbContext.Users.FirstOrDefaultAsync(a => a.AccountId == accountId && a.Id == userId);
                    if (user != null)
                    {
                        result = new UserResponse
                        {
                            Id = user.Id,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Email = user.Email
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return result;
        }

        public async Task<CustomResponse> UpdateUserAsync(int accountId, int userId,UserRequest userRequest)
        {
            CustomResponse result = new CustomResponse();
            try
            {
                var account = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);
                if (account == null)
                {
                    return new CustomResponse
                    {
                        ResponseCode = "01",
                        ResponseMessage = "Account Not Found"
                    };
                }
                else
                {
                    var user = await _dbContext.Users.FirstOrDefaultAsync(a => a.AccountId == accountId && a.Id == userId);
                    if (user == null)
                    {
                        return new CustomResponse
                        {
                            ResponseCode = "01",
                            ResponseMessage = "User Not Found"
                        };
                    }
                    else
                    {
                        user.FirstName = string.IsNullOrEmpty(userRequest.FirstName) ? user.FirstName : userRequest.FirstName;
                        user.LastName = string.IsNullOrEmpty(userRequest.LastName) ? user.LastName : userRequest.LastName;
                        user.Email = string.IsNullOrEmpty(userRequest.Email) ? user.Email : userRequest.Email;

                        _dbContext.Entry(user).State = EntityState.Modified;
                        await _dbContext.SaveChangesAsync();

                        result = new CustomResponse
                        {
                            ResponseCode = "00",
                            ResponseMessage = "User Details Updated"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                result = new CustomResponse
                {
                    ResponseCode = "01",
                    ResponseMessage = "Error Updating User"
                };
            }
            return result;
        }
    }
}
