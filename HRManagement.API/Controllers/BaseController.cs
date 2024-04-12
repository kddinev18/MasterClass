using HRManagement.Infrastructure.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HRManagement.API.Controllers
{
    public class BaseController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICurrentUserService _currentUserService;
        public BaseController(ICurrentUserService currentUserService, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _currentUserService = currentUserService;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            bool hasAllowAnonymous = filterContext.ActionDescriptor.EndpointMetadata
                                 .Any(em => em.GetType() == typeof(AllowAnonymousAttribute));

            if (!hasAllowAnonymous)
            {
                _currentUserService.User = _userManager.GetUserAsync(User).GetAwaiter().GetResult();
            }
        }
    }
}
