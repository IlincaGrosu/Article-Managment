using Application.Auth.Models;
using Microsoft.AspNetCore.Identity;
namespace Application.Auth.Services
{
    public interface IAuthenticationService
    {
        Task<string> AuthenticateUserAsync(string username, string password);
    }

    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;

        public AuthenticationService(UserManager<User> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<string> AuthenticateUserAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null || !(await _userManager.CheckPasswordAsync(user, password)))
            {
                return null;
            }

            return _tokenService.GenerateToken(user);
        }
    }
}
