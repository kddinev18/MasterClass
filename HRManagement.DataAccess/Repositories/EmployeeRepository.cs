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
    }
}
