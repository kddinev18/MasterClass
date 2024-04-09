using HRManagement.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagement.Infrastructure.Contracts
{
    public interface IEmployeeService : IBaseService
    {
        IQueryable<EmployeeDTO> GetAll(int pageNumber, int pageSize);
    }
}
