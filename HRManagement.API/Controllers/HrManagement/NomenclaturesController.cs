using HRManagement.Domain.Constants;
using HRManagement.Infrastructure.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.API.Controllers.HrManagement
{
    [ApiController]
    [Route("api/HrManagement/[controller]/[action]")]
    [Authorize(Roles = nameof(Roles.HR))]
    public class NomenclaturesController : BaseController
    {
        private readonly INomenclatureService _nomenclatureService;
        public NomenclaturesController(INomenclatureService nomenclatureService, ICurrentUserService currentUserService, UserManager<IdentityUser> userManager) : base(currentUserService, userManager)
        {
            _nomenclatureService = nomenclatureService;
        }

        [HttpGet]
        public IActionResult GetJobs()
        {
            return Ok(_nomenclatureService.GetJobs());
        }

        [HttpGet]
        public IActionResult GetDepartments()
        {
            return Ok(_nomenclatureService.GetDepartments());
        }

        [HttpGet]
        public IActionResult GetManagers()
        {
            return Ok(_nomenclatureService.GetManagers());
        }
    }
}
