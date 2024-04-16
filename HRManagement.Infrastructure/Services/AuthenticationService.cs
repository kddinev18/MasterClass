using HRManagement.Domain.Constants;
using HRManagement.Domain.DTO.Common.Request;
using HRManagement.Domain.DTO.Common.Response;
using HRManagement.Domain.Enums;
using HRManagement.Infrastructure.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HRManagement.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<IdentityUser> _userManager;
        public AuthenticationService(IConfiguration config, UserManager<IdentityUser> userManager)
        {
            _config = config;
            _userManager = userManager;
        }

        public async Task<TokenDTO> Register(RegisterDTO registerInfo)
        {
            IdentityUser user = new IdentityUser()
            {
                Email = registerInfo.Email,
                UserName = registerInfo.UserName,
            };
            var result = await _userManager.CreateAsync(user, registerInfo.Password);

            if (result.Succeeded)
            {
                if ((await _userManager.AddToRoleAsync(user, nameof(Roles.HR))).Succeeded)
                {
                    return await LogIn(new LogInDTO()
                    {
                        UserName = registerInfo.UserName,
                        Password = registerInfo.Password
                    });
                }
            }

            throw new UnauthorizedAccessException(string.Join('\n', result.Errors.Select(x => $"{x.Code}: {x.Description}")));
        }

        public async Task<TokenDTO> LogIn(LogInDTO logInInfo)
        {
            var user = await _userManager.FindByNameAsync(logInInfo.UserName);

            if (user != null)
            {
                if (await _userManager.CheckPasswordAsync(user, logInInfo.Password))
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    return GenerateToken(user, roles);
                }
            }

            throw new UnauthorizedAccessException("Wrong credentials");
        }

        private TokenDTO GenerateToken(IdentityUser user, IEnumerable<string> roles)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config[Jwt.Key]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName)
            };
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var token = new JwtSecurityToken(
                _config[Jwt.Issuer],
                _config[Jwt.Audience],
                claims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: credentials
            );

            return new TokenDTO()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expires = DateTime.Now.AddDays(2),
            };
        }

    }
}
