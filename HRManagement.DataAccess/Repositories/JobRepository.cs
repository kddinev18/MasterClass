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
    internal class JobRepository : BaseRepository<Job>, IJobRepository
    {
        public JobRepository(HrManagementContext db) : base(db)
        {
        }

        public override void UpdateEntity(Job oldEntity, Job newEntity)
        {
            oldEntity.Title = newEntity.Title;
            oldEntity.Salary = newEntity.Salary;
        }
    }
}
