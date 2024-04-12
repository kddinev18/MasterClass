using HRManagement.DAL.Data.Contracts;
using HRManagement.DAL.Data;
using HRManagement.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
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
        protected DbSet<TEntity> _entities;
        public BaseRepository(HrManagementContext db)
        {
            _db = db;
            _entities = db.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _entities;
        }

        public virtual IQueryable<TEntity> GetById(int id)
        {
            return _entities.Where(x => x.Id == id);
        }

        public virtual int AddOrUpdate(TEntity entity)
        {
            if (entity == null)
            {
                return 0;
            }

            if (entity.Id == 0)
            {
                entity.CreatedOn = DateTime.Now;
                entity.CreatedBy = "SYSTEM";
                _entities.Add(entity);
            }
            else
            {
                entity.UpdatedOn = DateTime.Now;
                entity.UpdatedBy = "SYSTEM";
                _entities.Update(entity);
            }

            return _db.SaveChanges();
        }

        public virtual int Delete(int id)
        {
            TEntity entity = _entities.Where(x => x.Id == id).FirstOrDefault();

            if (entity == null)
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
