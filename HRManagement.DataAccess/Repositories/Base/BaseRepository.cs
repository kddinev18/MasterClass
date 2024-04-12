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
using Microsoft.AspNetCore.Identity;

namespace HRManagement.DAL.Repositories.Base
{
    public abstract class BaseRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly HrManagementContext _db;
        protected readonly DbSet<TEntity> _entities;
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

        public virtual int AddOrUpdate(TEntity entity, IdentityUser user)
        {
            if (entity == null)
            {
                return 0;
            }

            if (entity.Id == 0)
            {
                entity.CreatedOn = DateTime.Now;
                entity.CreatedBy = user.UserName;
                _entities.Add(entity);
                _db.SaveChanges();

                return entity.Id;
            }
            else
            {
                TEntity dbEntity = GetById(entity.Id).First();
                UpdateEntity(dbEntity, entity);
                dbEntity.UpdatedOn = DateTime.Now;
                dbEntity.UpdatedBy = user.UserName;
                return _db.SaveChanges();
            }
        }

        public virtual int Delete(int id, IdentityUser user)
        {
            TEntity entity = GetById(id).FirstOrDefault();

            if (entity == null)
            {
                return 0;
            }

            entity.IsActive = false;
            entity.UpdatedOn = DateTime.Now;
            entity.UpdatedBy = user.UserName;
            DeleteAdditionalDependacies();

            return _db.SaveChanges();
        }

        public virtual void DeleteAdditionalDependacies() { }
        public abstract void UpdateEntity(TEntity oldEntity, TEntity newEntity);
    }
}
