using HRManagement.DAL.Models;
using HRManagement.DAL.Models.Contracts;
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
        private Dictionary<Type, Lazy<IGenericRepository<IEntity>>> _repositoryMap;
        public UnitOfWork(HrManagementContext db) 
        { 
            _db = db;
            IEnumerable<Type> repositoyTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(BaseRepository<IEntity>)));

            foreach (Type type in repositoyTypes)
            {
                _repositoryMap.Add(type, new Lazy<IGenericRepository<IEntity>>(()=>
                {
                    ConstructorInfo constructorInfo = type.GetConstructor(new Type[] { typeof(HrManagementContext) });
                    return constructorInfo.Invoke(new object[] { _db }) as IGenericRepository<IEntity>;
                }));
            }
        }

        public IGenericRepository<IEntity> GetRepository<TRepository>() where TRepository : class, IGenericRepository<IEntity>
        {
            Type repositoryType = typeof(TRepository);
            return _repositoryMap[repositoryType].Value;
        }
    }
}
