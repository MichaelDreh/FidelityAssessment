using AssessmentProject.Core;
using AssessmentProject.Core.DTOs.User.Request;
using AssessmentProject.Core.DTOs.User.Response;
using AssessmentProject.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssessmentProject.Controllers
{
    [Route("api")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("accounts/{id}/users")]
        public async Task<ActionResult<List<UserResponse>>> GetAccountUsers([BindRequired] int id)
        {
            var result = await _userService.GetAccountUsersAsync(id);
            return Ok(result);
        }

        [HttpGet("accounts/{aId}/users/{uId}")]
        public async Task<ActionResult<UserResponse>> GetAccountById([BindRequired] int aId, [BindRequired] int uId)
        {
            var result = await _userService.GetUserByIdAysnc(aId, uId);
            return Ok(result);
        }

        [HttpPost("accounts/{id}/users")]
        public async Task<ActionResult<CustomResponse>> CreateUser(int id, UserRequest user)
        {
            var result = await _userService.CreateUserAsync(id, user);
            return Ok(result);
        }

        [HttpDelete("accounts/{aId}/users/{uId}")]
        public async Task<ActionResult<CustomResponse>> DeleteUser([BindRequired] int aId, [BindRequired] int uId)
        {
            var result = await _userService.DeleteUserAync(aId, uId);
            return Ok(result);
        }

        [HttpPut("accounts/{aId}/users/{uId}")]
        public async Task<ActionResult<CustomResponse>> UpdateUser([BindRequired] int aId, [BindRequired] int uId, UserRequest user)
        {
            var result = await _userService.UpdateUserAsync(aId, uId, user);
            return Ok(result);
        }
    }
}
