using HRManagement.DAL.Data.Entities;
using HRManagement.DAL.Repositories;
using HRManagement.DAL.Repositories.Base;
using HRManagement.DAL.Repositories.Contracts;
using HRManagement.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using HRManagement.Infrastructure.Contracts;

namespace HRManagement.Controllers.API
{
    [ApiController]
    [Route("api/[controller]/[action]")]
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
    }
}
