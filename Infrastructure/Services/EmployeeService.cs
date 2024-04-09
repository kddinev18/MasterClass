using HRManagement.DAL.Repositories.Contracts;
using HRManagement.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRManagement.DAL.Repositories.Base;
using HRManagement.Domain.DTO;
using HRManagement.DAL.Data.Entities;

namespace HRManagement.Infrastructure.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository _employeeRepository;
        public EmployeeService(UnitOfWork unitOfWork)
        {
            _employeeRepository = unitOfWork.GetRepository<Employee>() as IEmployeeRepository;
        }

        public IQueryable<EmployeeDTO> GetAll(int pageNumber, int pageSize)
        {
            return _employeeRepository.GetAll()
                .Skip((pageNumber-1)*pageSize)
                .Take(pageSize)
                .Select(x=>new EmployeeDTO()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    ManagerName = x.Manager != null ? x.Manager.FirstName + " " + x.Manager.LastName : "",
                    JobTitle = x.Job.Title,
                    DepartmentName = x.Department.Name,
                });
        }
    }
}
