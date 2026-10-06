using Futsal_Management.Domain.ViewModel;
using Futsal_Management.Domain.Enum;
using Futsal_Management.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Futsal_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(
        Roles = UserGroupRole.SuperAdmin + "," +
                UserGroupRole.Admin)]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Create-User")]
        public async Task<IActionResult> CreateUser(
            [FromBody] UserDto dto)
        {
            var result = await _userService.CreateUser(dto);

            if (!result.Status)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var result = await _userService.GetUserById(id);

            if (!result.Status)
                return NotFound(result);

            return Ok(result);
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _userService.GetUsers();

            if (!result.Status)
                return NotFound(result);

            return Ok(result);
        }

        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(
            int id,
            [FromBody] UserDto dto)
        {
            var result = await _userService.UpdateUser(id, dto);

            if (!result.Status)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUser(id);

            if (!result.Status)
                return NotFound(result);

            return Ok(result);
        }
    }
}