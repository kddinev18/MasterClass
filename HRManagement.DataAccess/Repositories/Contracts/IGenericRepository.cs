using Microsoft.AspNetCore.Identity;

namespace HRManagement.DAL.Repositories.Contracts
{
    public interface IGenericRepository<TEntity>
    {
        public abstract IQueryable<TEntity> GetAll();

        public abstract IQueryable<TEntity> GetById(int id);

        public abstract int AddOrUpdate(TEntity entity, IdentityUser User);

        public abstract int Delete(int id, IdentityUser User);
    }
}
