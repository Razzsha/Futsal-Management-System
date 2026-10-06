using Futsal_Management.Domain.ViewModel;
using Futsal_Management.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Futsal_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginDto loginDto)
        {
            var result = await _authService.Login(loginDto);

            if (!result.Status)
            {
                return Unauthorized(result);
            }

            return Ok(result);
        }
    }
}