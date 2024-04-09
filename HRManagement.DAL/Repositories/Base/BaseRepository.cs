using HRManagement.DAL.Models;
using HRManagement.DAL.Models.Contracts;
using HRManagement.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagement.DAL.Repositories.Base
{
    public class BaseRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        protected HrManagementContext _db;
        public BaseRepository(HrManagementContext db)
        {
            _db = db;
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _db.Set<TEntity>();
        }

        public virtual TEntity? GetById(int id)
        {
            return _db.Set<TEntity>().Where(x => x.Id == id).FirstOrDefault();
        }

        public virtual int AddOrUpdate(TEntity entity)
        {
            if (entity == null)
            {
                return 0;
            }

            if(entity.Id == 0)
            {
                entity.CreatedOn = DateTime.Now;
                entity.CreatedBy = "SYSTEM";
                _db.Set<TEntity>().Add(entity);
            }
            else
            {
                entity.UpdatedOn = DateTime.Now;
                entity.UpdatedBy = "SYSTEM";
                _db.Set<TEntity>().Update(entity);
            }

            return _db.SaveChanges();
        }

        public virtual int Delete(int id)
        {
            TEntity entity = _db.Set<TEntity>().Where(x => x.Id == id).FirstOrDefault();

            if(entity == null)
            {
                return 0;
            }

            entity.IsActive = false;
            entity.UpdatedOn = DateTime.Now;
            entity.UpdatedBy = "SYSTEM";

            return _db.SaveChanges();
        }
    }
}
