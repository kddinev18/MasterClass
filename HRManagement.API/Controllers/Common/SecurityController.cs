using HRManagement.Domain.Constants;
using HRManagement.Domain.DTO.Common.Request;
using HRManagement.Infrastructure.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection;
using System.Runtime.CompilerServices;

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
        public async Task<ActionResult> Register([FromBody] RegisterDTO registerInfo)
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
        public async Task<ActionResult> LogIn([FromBody] LogInDTO logInInfo)
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
