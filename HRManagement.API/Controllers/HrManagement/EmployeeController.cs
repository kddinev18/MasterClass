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
    public class EmployeeController : Controller
    {
        private IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
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
        public IActionResult Add([FromBody] EmployeeRequestDTO employee)
        {
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] EmployeeRequestDTO employee)
        {
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] int id)
        {
            return Ok();
        }
    }
}
