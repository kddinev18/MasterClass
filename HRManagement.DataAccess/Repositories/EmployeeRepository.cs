using HRManagement.DAL.Data;
using HRManagement.DAL.Data.Entities;
using HRManagement.DAL.Repositories.Base;
using HRManagement.DAL.Repositories.Contracts;
using HRManagement.DataAccess.Repositories.Base;
using HRManagement.Domain.Enums;
using HRManagement.Domain.Filters;
using HRManagement.Domain.Filters.Base;

namespace HRManagement.DAL.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(HrManagementContext db) : base(db)
        {
        }

        public override void DeleteAdditionalDependencies(Employee employee)
        {
            foreach (JobHistory jobHistory in _db.JobHistories.Where(jobHistory => jobHistory.EmployeeId == employee.Id))
            {
                jobHistory.IsActive = false;
                jobHistory.UpdatedOn = employee.UpdatedOn;
                jobHistory.UpdatedBy = employee.UpdatedBy;
            }
        }

        public override void UpdateEntity(Employee oldEntity, Employee newEntity)
        {
            oldEntity.FirstName = newEntity.FirstName;
            oldEntity.LastName = newEntity.LastName;
            oldEntity.Email = newEntity.Email;
            oldEntity.PhoneNumber = newEntity.PhoneNumber;
            oldEntity.HireDate = newEntity.HireDate;
            oldEntity.JobId = newEntity.JobId;
            oldEntity.ManagerId = newEntity.ManagerId;
            oldEntity.DepartmentId = newEntity.DepartmentId;
        }

        public IQueryable<Employee> GetAllFiltered(BaseFilter<EmployeeFilters> filters)
        {
            IQueryable<Employee> query = GetAll();

            if (filters.Filters != null)
            {
                EmployeeFilters employeeFilters = filters.Filters;

                if (!string.IsNullOrEmpty(employeeFilters.FirstName))
                {
                    query = query.Where(e => e.FirstName.Contains(employeeFilters.FirstName));
                }

                if (!string.IsNullOrEmpty(employeeFilters.LastName))
                {
                    query = query.Where(e => e.LastName.Contains(employeeFilters.LastName));
                }

                if (!string.IsNullOrEmpty(employeeFilters.Email))
                {
                    query = query.Where(e => e.Email.Contains(employeeFilters.Email));
                }

                if (!string.IsNullOrEmpty(employeeFilters.PhoneNumber))
                {
                    query = query.Where(e => e.PhoneNumber.Contains(employeeFilters.PhoneNumber));
                }

                if (employeeFilters.HireDate != null)
                {
                    query = query.Where(e => e.HireDate.Date == employeeFilters.HireDate.Value.Date);
                }

                if (employeeFilters.JobId != null)
                {
                    query = query.Where(e => e.JobId == employeeFilters.JobId);
                }

                if (employeeFilters.ManagerId != null)
                {
                    query = query.Where(e => e.ManagerId == employeeFilters.ManagerId);
                }

                if (employeeFilters.DepartmentId != null)
                {
                    query = query.Where(e => e.DepartmentId == employeeFilters.DepartmentId);
                }
            }

            if (!string.IsNullOrEmpty(filters.SortBy))
            {
                if (filters.SortDirection == null || filters.SortDirection == SortOrder.ASC)
                {
                    query = query.OrderByWithColumnName(filters.SortBy);
                }
                else if (filters.SortDirection == SortOrder.DESC)
                {
                    query = query.OrderByWithColumnNameDescending(filters.SortBy);
                }
            }

            return query
                .Skip((filters.Page - 1) * filters.PageSize)
                .Take(filters.PageSize);
        }
    }
}
