using HRManagement.DAL.Data.Entities;
using HRManagement.Domain.Filters;
using HRManagement.Domain.Filters.Base;

namespace HRManagement.DAL.Repositories.Contracts
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        IQueryable<Employee> GetAllFiltered(BaseFilter<EmployeeFilters> filters);
        IQueryable<Employee> PageResult(BaseFilter<EmployeeFilters> filters);
    }
}
