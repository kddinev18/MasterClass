﻿using HRManagement.DAL.Data;
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
    public class JobHistoryRepository : BaseRepository<JobHistory>, IJobHistoryRepository
    {
        public JobHistoryRepository(HrManagementContext db) : base(db)
        {
        }
    }
}
