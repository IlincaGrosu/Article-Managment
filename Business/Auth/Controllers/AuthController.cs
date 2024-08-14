using Application.Auth.Services;
using Business.Auth.Models;
using Microsoft.AspNetCore.Mvc;

namespace Business.Auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var token = await _authenticationService.AuthenticateUserAsync(model.Username, model.Password);
            if (token == null)
                return Unauthorized();

            return Ok(new { Token = token });
        }
    }
}
