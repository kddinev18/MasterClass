using HRManagement.Domain.Constants;
using HRManagement.Domain.DTO.HrManagement.Request;
using HRManagement.Domain.Filters;
using HRManagement.Domain.Filters.Base;
using HRManagement.Infrastructure.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.API.Controllers.HrManagement
{
    [ApiController]
    [Route("api/HrManagement/[controller]/[action]")]
    [Authorize(Roles = nameof(Roles.HR))]
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService, ICurrentUserService currentUserService, UserManager<IdentityUser> userManager) : base(currentUserService, userManager)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult GetAll([FromBody] BaseFilter<EmployeeFilters> filters)
        {
            return Ok(_employeeService.GetAll(filters));
        }

        [HttpGet]
        public IActionResult Get([FromQuery] int id)
        {
            return Ok(_employeeService.Get(id));
        }

        [HttpPost]
        public IActionResult AddOrUpdate([FromBody] EmployeeRequestDTO employee)
        {
            return Ok(_employeeService.AddOrUpdate(employee));
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] int id)
        {
            return Ok(_employeeService.Delete(id));
        }
    }
}
