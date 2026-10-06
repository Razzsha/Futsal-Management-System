using Futsal_Management.Domain.ViewModel;
using Futsal_Management.Domain.Enum;
using Futsal_Management.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Futsal_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserGroupRole.SuperAdmin)]
    public class UserGroupController : ControllerBase
    {
        private readonly IUserGroupService _userGroupService;

        public UserGroupController(
            IUserGroupService userGroupService)
        {
            _userGroupService = userGroupService;
        }

        [HttpPost("Create-Group")]
        public async Task<IActionResult> CreateUserGroup(
            [FromBody] UserGroupDto dto)
        {
            var result = await _userGroupService.CreateUserGroup(dto);

            if (!result.Status)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("AllGroup")]
        public async Task<IActionResult> GetUserGroups()
        {
            var result = await _userGroupService.GetUserGroups();

            if (!result.Status)
                return NotFound(result);

            return Ok(result);
        }

        [HttpGet("GetGroupById/{id}")]
        public async Task<IActionResult> GetUserGroupById(int id)
        {
            var result = await _userGroupService.GetUserGroupById(id);

            if (!result.Status)
                return NotFound(result);

            return Ok(result);
        }

        [HttpPut("UpdateGroup/{id}")]
        public async Task<IActionResult> UpdateUserGroup(
            int id,
            [FromBody] UserGroupDto dto)
        {
            var result = await _userGroupService
                .UpdateUserGroup(id, dto);

            if (!result.Status)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("DeleteGroup/{id}")]
        public async Task<IActionResult> DeleteUserGroup(int id)
        {
            var result = await _userGroupService.DeleteUserGroup(id);

            if (!result.Status)
                return BadRequest(result);

            return Ok(result);
        }
    }
}