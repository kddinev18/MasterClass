using HRManagement.Domain.DTO.Common;
using HRManagement.Infrastructure.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.API.Controllers.Common
{

    [ApiController]
    [Route($"api/Common/[controller]/[action]")]
    public class SecurityController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;
        public SecurityController(IAuthenticationService authenticationService, ICurrentUserService currentUserService, UserManager<IdentityUser> userManager) : base(currentUserService, userManager)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerInfo)
        {
            try
            {
                var result = await _authenticationService.Register(registerInfo);
                return Ok(result);
            }
            catch (UnauthorizedAccessException e)
            {
                return Unauthorized(e.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LogIn([FromBody] LogInDTO logInInfo)
        {
            try
            {
                var result = await _authenticationService.LogIn(logInInfo);
                return Ok(result);
            }
            catch (UnauthorizedAccessException e)
            {
                return Unauthorized(e.Message);
            }
        }
    }
}
