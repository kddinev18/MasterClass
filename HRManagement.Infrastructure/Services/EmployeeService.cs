using HRManagement.DAL.Repositories.Contracts;
using HRManagement.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRManagement.DAL.Repositories.Base;
using HRManagement.DAL.Data.Entities;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using HRManagement.Domain.DTO.HrManagement.Request;
using HRManagement.Domain.DTO.HrManagement.Response;
using Microsoft.AspNetCore.Identity;

namespace HRManagement.Infrastructure.Services
{
    public class EmployeeService : BaseService, IEmployeeService
    {
        private IEmployeeRepository _employeeRepository;
        private IJobRepository _jobRepository;
        public EmployeeService(UnitOfWork unitOfWork, ICurrentUserService currentUserService) : base(currentUserService)
        {
            _employeeRepository = unitOfWork.GetRepository<Employee>() as IEmployeeRepository;
            _jobRepository = unitOfWork.GetRepository<Job>() as IJobRepository;
        }

        public IQueryable<EmployeeResponseDTO> GetAll(int pageNumber, int pageSize)
        {
            return _employeeRepository.GetAll()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new EmployeeResponseDTO()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    ManagerName = x.Manager != null ? x.Manager.FirstName + " " + x.Manager.LastName : null,
                    JobTitle = x.Job.Title,
                    DepartmentName = x.Department.Name,
                });
        }

        public EmployeeResponseDTO Get(int id)
        {
            return _employeeRepository
                .GetById(id)
                .Select(employee=>
                    new EmployeeResponseDTO()
                    {
                        Id = employee.Id,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        Email = employee.Email,
                        PhoneNumber = employee.PhoneNumber,
                        ManagerName = employee.Manager != null ? employee.Manager.FirstName + " " + employee.Manager.LastName : null,
                        JobTitle = employee.Job.Title,
                        DepartmentName = employee.Department.Name,
                    }
                )
                .First();
        }

        public int AddOrUpdate(EmployeeRequestDTO employee)
        {
            if(!employee.JobExsists)
            {
                employee.JobId = _jobRepository.AddOrUpdate(new Job()
                {
                    Title = employee.JobTitle
                }, _currentUserService.User);
            }
            return _employeeRepository.AddOrUpdate(new Employee()
            {
                Id = employee.Id.HasValue ? employee.Id.Value : 0,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                JobId = employee.JobId.Value,
                ManagerId = employee.ManagerId,
                DepartmentId = employee.DepartmentId,
                HireDate = employee.HireDate
            }, _currentUserService.User);
        }

        public int Delete(int id)
        {
            return _employeeRepository.Delete(id, _currentUserService.User);
        }
    }
}
