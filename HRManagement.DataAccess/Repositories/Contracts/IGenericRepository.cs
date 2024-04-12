using HRManagement.DAL.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagement.DAL.Repositories.Contracts
{
    public interface IGenericRepository<TEntity>
    {
        public abstract IQueryable<TEntity> GetAll();

        public abstract IQueryable<TEntity> GetById(int id);

        public abstract int AddOrUpdate(TEntity entity);

        public abstract int Delete(int id);
    }
}
