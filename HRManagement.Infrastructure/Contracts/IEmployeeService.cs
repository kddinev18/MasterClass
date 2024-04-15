﻿using HRManagement.Domain.DTO.HrManagement.Request;
using HRManagement.Domain.DTO.HrManagement.Response;
using HRManagement.Domain.Filters;
using HRManagement.Domain.Filters.Base;

namespace HRManagement.Infrastructure.Contracts
{
    public interface IEmployeeService : IBaseService
    {
        int AddOrUpdate(EmployeeRequestDTO employee);
        int Delete(int id);
        EmployeeResponseDTO Get(int id);
        IQueryable<EmployeeResponseDTO> GetAll(BaseFilter<EmployeeFilters> filters);
        int Promote(PromoteDTO promote);
    }
}
