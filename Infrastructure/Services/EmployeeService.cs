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
        private IJobRepository _jobRepository;
        private IDepartmentRepository _departmentRepository;
        public EmployeeService(UnitOfWork unitOfWork)
        {
            _employeeRepository = unitOfWork.GetRepository<Employee>() as IEmployeeRepository;
            _jobRepository = unitOfWork.GetRepository<Job>() as IJobRepository;
            _departmentRepository = unitOfWork.GetRepository<Department>() as IDepartmentRepository; 
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
                    ManagerName = x.ManagerId.HasValue ? _employeeRepository.GetById(x.ManagerId.Value).FirstName : "",
                    JobTitle = _jobRepository.GetById(x.JobId).Title,
                    DepartmentName = _departmentRepository.GetById(x.DepartmentId).Name
                });
        }
    }
}
