using HRManagement.DAL.Data.Contracts;
using HRManagement.DAL.Data;
using HRManagement.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HRManagement.DAL.Repositories.Base
{
    public class UnitOfWork
    {
        private HrManagementContext _db;
        private Dictionary<Type, object> _repositoriesMap;
        public UnitOfWork(HrManagementContext db) 
        { 
            _db = db;
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

            object repositoryInstance = constructor.Invoke(new object[] { _db });
            _repositoriesMap.Add(typeof(TEntity), repositoryInstance);
            return repositoryInstance;
        }
    }
}
