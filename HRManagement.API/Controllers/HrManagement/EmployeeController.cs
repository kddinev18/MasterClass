using HRManagement.DAL.Data.Entities;
using HRManagement.DAL.Repositories;
using HRManagement.DAL.Repositories.Base;
using HRManagement.DAL.Repositories.Contracts;
using HRManagement.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using HRManagement.Infrastructure.Contracts;
using Microsoft.AspNetCore.Authorization;
using HRManagement.Domain.Constants;
using Microsoft.AspNetCore.Identity;
using HRManagement.Domain.DTO.HrManagement.Request;

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
        public IActionResult GetAll([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            return Ok(_employeeService.GetAll(pageNumber, pageSize));
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
