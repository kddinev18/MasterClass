using HRManagement.Domain.DTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagement.Infrastructure.Contracts
{
    public interface INomenclatureService : IBaseService
    {
        IQueryable<NomenclatureDTO<int>> GetDepartments();
        IQueryable<NomenclatureDTO<int>> GetJobs();
    }
}
