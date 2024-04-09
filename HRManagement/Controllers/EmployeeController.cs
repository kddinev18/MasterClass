using HRManagement.DAL.Data.Entities;
using HRManagement.DAL.Repositories;
using HRManagement.DAL.Repositories.Base;
using HRManagement.DAL.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EmployeeController : Controller
    {
        private IEmployeeRepository _employeeRepository;
        public EmployeeController(UnitOfWork unitOfWork)
        {
            _employeeRepository = unitOfWork.GetRepository<Employee>() as IEmployeeRepository;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            return Ok(_employeeRepository.GetAll().Skip((page-1)*pageSize).Take(pageSize));
        }


        [HttpPost]
        public IActionResult Create([FromBody] Employee employee)
        {
            return Ok(_employeeRepository.AddOrUpdate(employee));
        }


        [HttpGet]
        public IActionResult GetEmployeeManagerId([FromQuery] int id)
        {
            return Ok(_employeeRepository.GetEmployeeManagerId(id));
        }
    }
}
