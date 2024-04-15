using HRManagement.DAL.Data;
using HRManagement.DAL.Data.Entities;
using HRManagement.DAL.Repositories.Base;
using HRManagement.DAL.Repositories.Contracts;
using HRManagement.Domain.Filters;

namespace HRManagement.DAL.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(HrManagementContext db) : base(db)
        {
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

        public IQueryable<Employee> ApplyFilters(IQueryable<Employee> query, EmployeeFilters? filters)
        {
            if (filters == null)
            {
                return query;
            }

            if (filters.FirstName != null)
            {
                query = query.Where(e => e.FirstName.Contains(filters.FirstName));
            }

            if (filters.LastName != null)
            {
                query = query.Where(e => e.LastName.Contains(filters.LastName));
            }

            if (filters.Email != null)
            {
                query = query.Where(e => e.Email.Contains(filters.Email));
            }

            if (filters.PhoneNumber != null)
            {
                query = query.Where(e => e.PhoneNumber.Contains(filters.PhoneNumber));
            }

            if (filters.HireDate != null)
            {
                query = query.Where(e => e.HireDate == filters.HireDate);
            }

            if (filters.JobId != null)
            {
                query = query.Where(e => e.JobId == filters.JobId);
            }

            if (filters.ManagerId != null)
            {
                query = query.Where(e => e.ManagerId == filters.ManagerId);
            }

            if (filters.DepartmentId != null)
            {
                query = query.Where(e => e.DepartmentId == filters.DepartmentId);
            }

            return query;
        }
    }
}
