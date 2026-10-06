using Futsal_Management.Domain.Enum;
using Futsal_Management.Domain.ViewModel;
using Futsal_Management.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace Futsal_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(
        Roles = UserGroupRole.SuperAdmin + "," +
                UserGroupRole.Admin + "," +
                UserGroupRole.Staff)]
    public class ArenaController : ControllerBase
    {
        private readonly IArenaService _arenaService;

        public ArenaController(IArenaService arenaService)
        {
            _arenaService = arenaService;
        }

        [HttpPost("CreateArena")]
        public async Task<IActionResult> CreateArena(
            [FromBody] ArenaDto dto)
        {
            var result = await _arenaService.CreateArena(dto);

            if (!result.Status)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("GetAllArena")]
        public async Task<IActionResult> GetAllArena()
        {
            var result = await _arenaService.GetAllArena();

            if (!result.Status)
                return NotFound(result);

            return Ok(result);
        }

        [HttpGet("GetArenaById/{id}")]
        public async Task<IActionResult> GetArenaById(int id)
        {
            var result = await _arenaService.GetArenaById(id);

            if (!result.Status)
                return NotFound(result);

            return Ok(result);
        }

        [HttpPut("UpdateArena/{id}")]
        public async Task<IActionResult> UpdateArena(
            int id,
            [FromBody] ArenaDto dto)
        {
            var result = await _arenaService.UpdateArena(id, dto);

            if (!result.Status)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("DeleteArena/{id}")]
        public async Task<IActionResult> DeleteArena(int id)
        {
            var result = await _arenaService.DeleteArena(id);

            if (!result.Status)
                return NotFound(result);

            return Ok(result);
        }
    }
}