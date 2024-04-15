using HRManagement.DAL.Data;
using HRManagement.DAL.Data.Contracts;
using System.Reflection;

namespace HRManagement.DAL.Repositories.Base
{
    public class UnitOfWork
    {
        public HrManagementContext Db { get; set; }
        private Dictionary<Type, object> _repositoriesMap;
        public UnitOfWork(HrManagementContext db)
        {
            Db = db;
            _repositoriesMap = new Dictionary<Type, object>();
        }

        public object GetRepository<TEntity>() where TEntity : class, IEntity
        {
            if (_repositoriesMap.ContainsKey(typeof(TEntity)))
            {
                return _repositoriesMap[typeof(TEntity)];
            }

            Type repositoryType = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => type.IsClass && !type.IsAbstract && typeof(BaseRepository<TEntity>).IsAssignableFrom(type)).First();
            ConstructorInfo constructor = repositoryType.GetConstructor(new Type[] { typeof(HrManagementContext) });

            object repositoryInstance = constructor.Invoke(new object[] { Db });
            _repositoriesMap.Add(typeof(TEntity), repositoryInstance);
            return repositoryInstance;
        }

        public BaseRepository<TEntity> GetRepository<TEntity>(bool isBaseRepository) where TEntity : class, IEntity
        {
            if (_repositoriesMap.ContainsKey(typeof(TEntity)))
            {
                return _repositoriesMap[typeof(TEntity)] as BaseRepository<TEntity>;
            }

            Type repositoryType = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => type.IsClass && !type.IsAbstract && typeof(BaseRepository<TEntity>).IsAssignableFrom(type)).First();
            ConstructorInfo constructor = repositoryType.GetConstructor(new Type[] { typeof(HrManagementContext) });

            object repositoryInstance = constructor.Invoke(new object[] { Db });
            _repositoriesMap.Add(typeof(TEntity), repositoryInstance);
            return repositoryInstance as BaseRepository<TEntity>;
        }
    }
}
