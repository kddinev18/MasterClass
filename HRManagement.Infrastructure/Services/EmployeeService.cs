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

namespace HRManagement.Infrastructure.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository _employeeRepository;
        public EmployeeService(UnitOfWork unitOfWork)
        {
            _employeeRepository = unitOfWork.GetRepository<Employee>() as IEmployeeRepository;
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

        public EmployeeResponseDTO Create(int id)
        {
            return _employeeRepository
                .GetById(id)
                .Select(employee =>
                    new EmployeeResponseDTO()
                    {
                        Id = employee.Id,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        Email = employee.Email,
                        PhoneNumber = employee.PhoneNumber,
                        ManagerName = employee.Manager != null ? employee.Manager.FirstName + " " + employee.Manager.LastName : "",
                        JobTitle = employee.Job.Title,
                        DepartmentName = employee.Department.Name,
                    }
                )
                .First();
        }
    }
}
