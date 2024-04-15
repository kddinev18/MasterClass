using HRManagement.DAL.Data.Entities;
using HRManagement.DAL.Repositories.Base;
using HRManagement.DAL.Repositories.Contracts;
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
        private readonly IJobRepository _jobRepository;
        private readonly IJobHistoryRepository _jobHistoryRepository;
        public EmployeeService(UnitOfWork unitOfWork, ICurrentUserService currentUserService) : base(currentUserService)
        {
            _employeeRepository = unitOfWork.GetRepository<Employee>() as IEmployeeRepository;
            _jobRepository = unitOfWork.GetRepository<Job>() as IJobRepository;
            _jobHistoryRepository = unitOfWork.GetRepository<JobHistory>() as IJobHistoryRepository;
        }

        public IQueryable<EmployeeResponseDTO> GetAll(BaseFilter<EmployeeFilters> filters)
        {
            return _employeeRepository.GetAllFiltered(filters)
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
                    PreviousJobs = _jobHistoryRepository.GetAll().Where(j => j.EmployeeId == x.Id).Select(j => j.Job.Title).ToList()
                });
        }

        public EmployeeResponseDTO Get(int id)
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
                        ManagerName = employee.Manager != null ? employee.Manager.FirstName + " " + employee.Manager.LastName : null,
                        JobTitle = employee.Job.Title,
                        DepartmentName = employee.Department.Name,
                    }
                )
                .First();
        }

        public int AddOrUpdate(EmployeeRequestDTO employee)
        {
            return _employeeRepository.AddOrUpdate(new Employee()
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
        }

        public int Delete(int id)
        {
            return _employeeRepository.Delete(id, _currentUserService.User);
        }

        public int Promote(PromoteDTO promote)
        {
            JobHistory oldJob = _jobHistoryRepository.GetAll()
                .Where(j => j.EmployeeId == promote.EmployeeId && j.JobId == promote.OldJobId)
                .First();
            oldJob.EndDate = DateTime.Now;
            _jobHistoryRepository.AddOrUpdate(oldJob, _currentUserService.User);

            return _jobHistoryRepository.AddOrUpdate(new JobHistory()
            {
                EmployeeId = promote.EmployeeId,
                JobId = promote.NewJobId,
                StartDate = DateTime.Now,
                DepartmentId = promote.NewDepartment.HasValue ? promote.NewDepartment.Value : oldJob.DepartmentId,
                EndDate = null
            }, _currentUserService.User);
        }
    }
}
