using HRManagement.Domain.DTO.HrManagement.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagement.Infrastructure.Contracts
{
    public interface IEmployeeService : IBaseService
    {
        EmployeeResponseDTO Get(int id);
        IQueryable<EmployeeResponseDTO> GetAll(int pageNumber, int pageSize);
    }
}
