using HRManagement.DAL.Data.Entities;
using HRManagement.DAL.Repositories.Base;
using HRManagement.DAL.Repositories.Contracts;
using HRManagement.Domain.DTO.Common;
using HRManagement.Domain.DTO.Common.Base;
using HRManagement.Domain.DTO.HrManagement.Request;
using HRManagement.Domain.DTO.HrManagement.Response;
using HRManagement.Domain.Filters;
using HRManagement.Domain.Filters.Base;
using HRManagement.Infrastructure.Contracts;

namespace HRManagement.Infrastructure.Services
{
    public class EmployeeService : BaseService, IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IJobHistoryRepository _jobHistoryRepository;
        public EmployeeService(UnitOfWork unitOfWork, ICurrentUserService currentUserService) : base(currentUserService)
        {
            _employeeRepository = unitOfWork.GetRepository<Employee>() as IEmployeeRepository;
            _jobHistoryRepository = unitOfWork.GetRepository<JobHistory>() as IJobHistoryRepository;
        }

        public BaseCollectionResponse<EmployeeResponseDTO> GetAll(BaseFilter<EmployeeFilters> filters)
        {
            return new BaseCollectionResponse<EmployeeResponseDTO>()
            {
                Items = _employeeRepository.PageResult(filters)
                .Select(employee => new EmployeeResponseDTO()
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    HireDate = employee.HireDate,
                    PhoneNumber = employee.PhoneNumber,
                    Manager = employee.Manager != null ? new NomenclatureDTO<int>()
                    {
                        Id = employee.Manager.Id,
                        Code = employee.Manager.Email.ToUpper(),
                        Value = employee.Manager.FirstName + " " + employee.Manager.LastName
                    } : null,
                    Job = new NomenclatureDTO<int>()
                    {
                        Id = employee.Job.Id,
                        Code = employee.Job.Title.ToUpper(),
                        Value = employee.Job.Title
                    },
                    Department = new NomenclatureDTO<int>()
                    {
                        Id = employee.Department.Id,
                        Code = employee.Department.Name.ToUpper(),
                        Value = employee.Department.Name
                    },
                    PreviousJobs = _jobHistoryRepository.GetAll()
                        .Where(job => job.EmployeeId == employee.Id && job.EndDate.HasValue)
                        .Select(job => new NomenclatureDTO<int>()
                        {
                            Id = employee.Job.Id,
                            Code = employee.Job.Title.ToUpper(),
                            Value = employee.Job.Title
                        })
                        .ToList()
                }),
                Count = _employeeRepository.GetAllFiltered(filters).Count()
            };
        }

        public EmployeeResponseDTO Get(int id)
        {
            return _employeeRepository
                .GetById(id)
                .Select(employee => new EmployeeResponseDTO()
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    HireDate = employee.HireDate,
                    Manager = employee.Manager != null ? new NomenclatureDTO<int>()
                    {
                        Id = employee.Manager.Id,
                        Code = employee.Manager.Email.ToUpper(),
                        Value = employee.Manager.FirstName + " " + employee.Manager.LastName
                    } : null,
                    Job = new NomenclatureDTO<int>()
                    {
                        Id = employee.Job.Id,
                        Code = employee.Job.Title.ToUpper(),
                        Value = employee.Job.Title
                    },
                    Department = new NomenclatureDTO<int>()
                    {
                        Id = employee.Department.Id,
                        Code = employee.Department.Name.ToUpper(),
                        Value = employee.Department.Name
                    },
                    PreviousJobs = _jobHistoryRepository.GetAll()
                    .Where(job => job.EmployeeId == employee.Id && job.EndDate.HasValue)
                    .Select(job => new NomenclatureDTO<int>()
                    {
                        Id = employee.Job.Id,
                        Code = employee.Job.Title.ToUpper(),
                        Value = employee.Job.Title
                    })
                    .ToList()
                })
                .First();
        }

        public int AddOrUpdate(EmployeeRequestDTO employee)
        {
            Employee dbEmployee = _employeeRepository.GetAddOrUpdate(new Employee()
            {
                Id = employee.Id.HasValue ? employee.Id.Value : 0,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                JobId = employee.JobId,
                ManagerId = employee.ManagerId,
                DepartmentId = employee.DepartmentId,
                HireDate = employee.HireDate
            }, _currentUserService.User);

            return _jobHistoryRepository.AddOrUpdate(new JobHistory()
            {
                EmployeeId = dbEmployee.Id,
                JobId = dbEmployee.JobId,
                StartDate = DateTime.Now,
                DepartmentId = dbEmployee.DepartmentId,
                EndDate = null
            }, _currentUserService.User);
        }

        public int Delete(int id)
        {
            return _employeeRepository.Delete(id, _currentUserService.User);
        }
    }
}
