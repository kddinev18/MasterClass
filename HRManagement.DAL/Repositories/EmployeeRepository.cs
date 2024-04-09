using HRManagement.DAL.Models;
using HRManagement.DAL.Models.Entities;
using HRManagement.DAL.Repositories.Base;
using HRManagement.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagement.DAL.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository<Employee>
    {
        public EmployeeRepository(HrManagementContext db) : base(db)
        {
        }
    }
}
