using HRManagement.Domain.DTO.HrManagement.Request;
using HRManagement.Domain.DTO.HrManagement.Response;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagement.Infrastructure.Contracts
{
    public interface IEmployeeService : IBaseService
    {
        int AddOrUpdate(EmployeeRequestDTO employee);
        int Delete(int id);
        EmployeeResponseDTO Get(int id);
        IQueryable<EmployeeResponseDTO> GetAll(int pageNumber, int pageSize);
    }
}
