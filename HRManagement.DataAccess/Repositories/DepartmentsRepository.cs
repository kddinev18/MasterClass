using HRManagement.DAL.Data;
using HRManagement.DAL.Data.Entities;
using HRManagement.DAL.Repositories.Base;
using HRManagement.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagement.DAL.Repositories
{
    public class DepartmentsRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentsRepository(HrManagementContext db) : base(db)
        {
        }
    }
}
