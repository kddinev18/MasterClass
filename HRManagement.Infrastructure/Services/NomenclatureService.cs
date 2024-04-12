using HRManagement.DAL.Data.Entities;
using HRManagement.DAL.Repositories.Base;
using HRManagement.DAL.Repositories.Contracts;
using HRManagement.Domain.DTO.Common;
using HRManagement.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagement.Infrastructure.Services
{
    public class NomenclatureService : BaseService, INomenclatureService
    {
        private IJobRepository _jobRepository;
        private IDepartmentRepository _departmentRepository;
        public NomenclatureService(UnitOfWork unitOfWork, ICurrentUserService currentUserService) : base(currentUserService)
        {
            _jobRepository = unitOfWork.GetRepository<Job>() as IJobRepository;
            _departmentRepository = unitOfWork.GetRepository<Job>() as IDepartmentRepository;
        }

        public IQueryable<NomenclatureDTO<int>> GetJobs()
        {
            return _jobRepository.GetAll()
                .Select(x =>
                    new NomenclatureDTO<int>()
                    {
                        Code = x.Id,
                        Value = x.Title
                    }
                );
        }

        public IQueryable<NomenclatureDTO<int>> GetDepartments()
        {
            return _departmentRepository.GetAll()
                .Select(x =>
                    new NomenclatureDTO<int>()
                    {
                        Code = x.Id,
                        Value = x.Name
                    }
                );
        }
    }
}
