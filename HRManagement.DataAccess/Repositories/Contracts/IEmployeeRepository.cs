using HRManagement.DAL.Data.Entities;
using HRManagement.Domain.Filters;

namespace HRManagement.DAL.Repositories.Contracts
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        IQueryable<Employee> ApplyFilters(IQueryable<Employee> query, EmployeeFilters? filters);
    }
}
